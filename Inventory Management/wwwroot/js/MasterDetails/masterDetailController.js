/// <reference path="../knockout.js" />

var masterdetailsController = function ()
{
    var self = this;
    const baseUrl = "/api/SalesDetailsAPI";
    self.SalesList = ko.observableArray([]);

    self.getData = function ()
    {
        ajax.get(baseUrl).then(function (result) {
            self.SalesList(result.map(item => new mastermodelVM(item)));
        });
    }
    self.getData();
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