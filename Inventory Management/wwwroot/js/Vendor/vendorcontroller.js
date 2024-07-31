﻿/// <reference path="vendermodel.js" />
/// <reference path="../knockout.js" />

const mode = {
    create: 1,
    update: 2
};

var VendorController = function () {
    var self = this;
    const baseUrl = '/api/VendorAPI';
    self.IsUpdated = ko.observable(false);
    self.NewVendor = ko.observable(new VendorModel());
    self.SelectedVendor = ko.observableArray([]);
    self.CurrentVendor = ko.observableArray([]);
    self.mode = ko.observable(mode.create);
    self.searchTerm = ko.observable('');
    self.searchArrayList = ko.observableArray([]);
    self.vendorToDelete = ko.observable();
    self.GetDatas = function () {
        ajax.get(baseUrl + "/GetVendorList").then(function (result) {
            self.CurrentVendor(result.map(item => new VendorModel(item))); // Corrected the mapping function
        });
    }

    self.GetDatas();

    self.getSearchData = function () {
        if (self.searchTerm()) {
            console.log("Searching for:", self.searchTerm());
            ajax.get(baseUrl + "/Search?searchTerm=" + encodeURIComponent(self.searchTerm()))
                .then(function (result) {
                    console.log("Search results:", result);
                    self.searchArrayList(result.map(item => new VendorModel(item)));
                    self.CurrentVendor(self.searchArrayList());
                    // self.currentPage(1); // Reset to first page after search
                })
                .fail(function (error) {
                    console.error("Search failed:", error);
                });
        } else {
            console.log("No search term, getting all data");
            self.GetDatas();
        }
    }

    self.clickedSearch = function () {
        self.getSearchData();
    }
    self.AddVendor = function () {
        var vendor = self.mode() == mode.create ? self.NewVendor() : self.SelectedVendor();
        if (vendor.Address() == '') {
    
            toastr.error("Address is required")
        }
        if (!vendor.isValid()){
            vendor.errors.showAllMessages();
            return;
        };



        var vendorData = ko.toJS(self.IsUpdated() ? self.SelectedVendor : self.NewVendor);
        switch (self.mode()) {
            case 1:
                ajax.post(baseUrl + "/AddVendor", JSON.stringify(vendorData))
                    .done(function (result) {
                        self.CurrentVendor.push(new VendorModel(result));
                        self.ResetForm();
                        self.CloseModel();
                        self.GetDatas();
                        toastr.success("Vendor Added SuccessFully !");
                        $('#vendorModal').modal('hide');
                    })
                break;
            case 2:
                ajax.put(baseUrl + "/UpdateVendor", JSON.stringify(vendorData))
                    .done(function (result) {
                        self.CurrentVendor.replace(self.SelectedVendor(), new VendorModel(result));
                        self.ResetForm();
                        //self.CloseModel();
                        self.GetDatas();
                        toastr.success("Vendor updated successFully ! ");
                        $('#vendorModal').modal('hide');
                    })
        }
    }


    self.DeleteVendor = function (model) {
        self.vendorToDelete(model);
        setTimeout(function () {
            $('#deleteConfirmModal').modal('show');
        }, 100);
    };

    self.confirmDelete = function () {
        var model = self.vendorToDelete();
        if (model) {
            ajax.delete(baseUrl + "/DeleteModel" + "?vendorId=" + model.Id())
                .done((result) => {
                    self.CurrentVendor.remove(model);
                    $('#deleteConfirmModal').modal('hide');
                })
                .fail((err) => {
                    console.log(err);
                    $('#deleteConfirmModal').modal('hide');
                });
            toastr.success("Vendor Deleted SuccessFully");
        }
    };

    self.SelectVendor = function (model) {
        self.SelectedVendor(model);
        self.IsUpdated(true);
        self.mode(mode.update);
        $('#vendorModel').modal('show');
    }

    self.CloseModel = function () {
        self.IsUpdated(false);
        self.SelectedVendor(new VendorModel());
        self.ResetForm();
        self.GetDatas();
    }

    self.ResetForm = function () {
        self.NewVendor(new VendorModel());
        self.SelectedVendor(new VendorModel());
        self.IsUpdated(false);
        self.SelectedVendor(new VendorModel());
    }
}


var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false,
        });
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