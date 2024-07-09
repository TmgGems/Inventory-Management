/// <reference path="../knockout.js" />

var masterdetailsController = function ()
{
    var self = this;
    const baseUrl = "/api/SalesDetailsAPI";
    self.SalesList = ko.observableArray([]);
    self.CustomersNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.NewSales = ko.observable(new mastermodelVM());
    self.SelectedSales = ko.observable(new mastermodelVM());
    self.IsUpdated = ko.observable(false);
    //self.NewSales().sales.push(new detailsmodelVM());
    //Get All Data
    self.getData = function ()
    {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.SalesList(result.map(item => new mastermodelVM(item)));
        });
    }
    self.getData();

    // Get CustomerNames

    self.getCustomersName = function () {
        var url = baseUrl + "/GetCustomersName";
        console.log("Fetching products from URL: " + url);

        return ajax.get(url).then(function (data) {
            // console.log("Products received: ", data);
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new customernamemodel(item);
            });
            self.CustomersNameList(mappedProducts);
            console.log("Customers Data: ", self.CustomersNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching customersname: ", textStatus, errorThrown);
        });
    };

    self.getCustomersName();

    //Get ItemNames
    self.getItemsName = function () {
        var url = baseUrl + "/GetItemsName";
        console.log("Fetching products from URL: " + url);

        return ajax.get(url).then(function (data) {
            // console.log("Products received: ", data);
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new itemnamemodel(item);
            });
            self.ItemsNameList(mappedProducts);
            console.log("Items Data: ", self.ItemsNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching itemsname: ", textStatus, errorThrown);
        });
    };

    self.getItemsName();

    self.AddSales = function ()
    {
        debugger;
        ajax.post(baseUrl + "/Add", ko.toJSON(self.NewSales()))
            .done(function (result) {
                self.SalesList.push(new mastermodelVM(result));
                self.resetForm();
                self.getData();
                $('#salesModal').modal('hide');
            })
            .fail(function (err) {
                console.log(err);
            });
    }

    self.resetForm = () => {
        self.NewSales(new mastermodelVM());
        self.IsUpdated(false);
    };


    self.AddItem = function () {
        self.NewSales().sales.push(new detailsmodelVM());
    };
    self.removeItem = function (item) {
        self.NewSales().sales.remove(item);
    };
};



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