/// <reference path="../knockout.js" />
var itemhistorycontroller = function () {
    var self = this;
    const baseUrl = "/api/ItemsInfoHistoryAPI";
    self.ItemHistoryList = ko.observableArray([]);
    self.searchTerm = ko.observable('');
    self.searchArrayList = ko.observableArray([]);
    self.transactionType = ko.observable('');

    self.getData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.ItemHistoryList(result.map(item => new itemhistorymodel(item)));
        });
    }

    self.getData();

    self.getSearchData = function ()
    {
        if (self.searchTerm()) {
            ajax.get(baseUrl + "/SearchItemName?searchedItem=" + encodeURIComponent(self.searchTerm()))
                .then(function (result) {
                    console.log("Search Results", result);
                    self.searchArrayList(result.map(item => new itemhistorymodel(item)));
                    self.ItemHistoryList(self.searchArrayList());
                })
        }
    }

    self.clickedSearch = function ()
    {
        self.getSearchData();
    }


    self.getSearchTransData = function ()
    {
        if (self.transactionType()) {
            ajax.get(baseUrl + "/SearchTransType?transType=" + encodeURIComponent(self.transactionType()))
                .then(function (result) {
                    self.searchArrayList(result.map(item => new itemhistorymodel(item)));
                    self.ItemHistoryList(self.searchArrayList());
                })
        }
    }
    self.clickTrans = function ()
    {
        self.getSearchTransData();
    }

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