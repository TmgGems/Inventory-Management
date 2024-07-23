﻿/// <reference path="../knockout.js" />

const mode = {
    create: 1,
    update: 2
};
var itemController = function () {
    var self = this;
    const baseUrl = "/api/ItemAPI";
    self.ItemList = ko.observableArray([]);
    self.NewItem = ko.observable(new itemModel());
    self.selectedList = ko.observable(new itemModel());
    self.IsUpdated = ko.observable(false);
    self.mode = ko.observable(mode.create);
    self.searchTerm = ko.observable('');
    self.searchArrayList = ko.observableArray([]);
    self.currentPage = ko.observable(1);
    self.itemsPerPage = ko.observable(10);
    self.totalPages = ko.computed(function () {
        return Math.ceil(self.ItemList().length / self.itemsPerPage());
    })


    //computed observable for paginated items
    self.pagedItems = ko.computed(function () {
        var startIndex = (self.currentPage() - 1) * self.itemsPerPage();
        return self.ItemList.slice(startIndex, startIndex + self.itemsPerPage());
    });

    // Navigation methods
    self.nextPage = function () {
        if (self.currentPage() < self.totalPages()) {
            self.currentPage(self.currentPage() + 1);
        }
    };

    self.previousPage = function () {
        if (self.currentPage() > 1) {
            self.currentPage(self.currentPage() - 1);
        }
    };

    self.goToPage = function (page) {
        self.currentPage(page);
    };




    self.getSearchData = function () {
        if (self.searchTerm()) {
            console.log("Searching for:", self.searchTerm());
            ajax.get(baseUrl + "/Search?searchTerm=" + encodeURIComponent(self.searchTerm()))
                .then(function (result) {
                    console.log("Search results:", result);
                    self.searchArrayList(result.map(item => new itemModel(item)));
                    self.ItemList(self.searchArrayList());
                    self.currentPage(1); // Reset to first page after search
                })
                .fail(function (error) {
                    console.error("Search failed:", error);
                });
        } else {
            console.log("No search term, getting all data");
            self.getData();
        }
    }

    self.clickedSearch = function () {
        self.getSearchData();
    }

    //self.filteredItemList = ko.computed(function () {
    //    var filter = self.searchTerm().toLowerCase();
    //    if (!filter) {
    //        return self.ItemList();
    //    } else {
    //        return ko.utils.arrayFilter(self.ItemList(), function (item) {
    //            return item.name().toLowerCase().indexOf(filter) !== -1;
    //        });
    //    }
    //});


    //self.getSearchData = function ()
    //{
    //    ajax.get(baseUrl + "/Search").then(function (result) {
    //        self.searchArrayList(result.map(item => new itemModel(item)))
    //    });
    //}

    //self.clickedSearch = function () {
    //    self.searchTerm(self.searchTerm());
    //    self.getSearchData();
    //}

    self.getData = function () {
        ajax.get(baseUrl).then(function (result) {
            self.ItemList(result.map(item => new itemModel(item)));
            self.currentPage(1); // Reset to first page when new data is loaded
        });
    }
    self.getData();

    self.AddItem = function () {
        switch (self.mode()) {
            case mode.create:
                debugger
                ajax.post(baseUrl, ko.toJSON(self.NewItem()))
                    .done(function (result) {
                        if (result.success) {
                            console.log(result.success);
                            self.ItemList.push(new itemModel(result.data));
                            self.resetForm();
                            self.getData();
                            $('#itemModal').modal('hide');
                        } else {
                            alert("Item name already exists!");
                        }
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            case mode.update:
                ajax.put(baseUrl, ko.toJSON(self.NewItem()))
                    .done(function (result) {
                        if (result.success) {
                            self.ItemList.replace(self.selectedList(), new itemModel(result));
                            self.resetForm();
                            self.getData();
                            $('#itemModal').modal('hide');
                        }
                        else {
                            alert("Name already exists!");
                        }
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            default:
                console.log("Invalid mode");
        }
    };

    self.Deleteitem = function (model) {
        ajax.delete(baseUrl + "?id=" + model.id())
            .done((result) => {
                self.ItemList.remove(model);
            })
            .fail((err) => {
                console.log(err);
            });
    };

    self.selectItem = function (model)
    {
        self.selectedList(model);
        self.NewItem(new itemModel(ko.toJS(model)));
        self.IsUpdated(true);
        self.mode(mode.update);
        $('#itemModal').modal('show');
    }

    self.setCreateMode = function () {
        self.resetForm();
        $('#itemModal').modal('show');
    };
    self.resetForm = () => {
        self.NewItem(new itemModel());
        self.IsUpdated(false);
        self.mode(mode.create);
    };
}

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false
        })
    },
    post: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            url: url,
            data: (data)
        });
    },
    put: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "PUT",
            url: url,
            data: data
        });
    },
    delete: function (route) {
        return $.ajax({
            method: "DELETE",
            url: route,
        });
    }
}