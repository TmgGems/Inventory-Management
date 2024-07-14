/// <reference path="../knockout.js" />

const mode = {
    create: 1,
    update: 2
};
var customerController = function () {
    var self = this;
    const baseUrl = "/api/CustomerAPI";
    self.CustomerList = ko.observableArray([]);
    self.NewCustomer = ko.observable(new customermodel());
    self.IsUpdated = ko.observable(false);
    self.SelectedCustomer = ko.observable(new customermodel());
    self.mode = ko.observable(mode.create);
    self.customerToDelete = ko.observable();


    //Fetch Data From Server
    self.getData = function () {
        ajax.get(baseUrl).then(function (result) {
            self.CustomerList(result.map(item => new customermodel(item)));
        });
    }
    self.getData();

    self.AddCustomer = function () {
        switch (self.mode()) {
            case mode.create:
                ajax.post(baseUrl, ko.toJSON(self.NewCustomer()))
                    .done(function (result) {
                        self.CustomerList.push(new customermodel(result));
                        self.resetForm();
                        self.getData();
                        //self.resetForm();
                        $('#customerModal').modal('hide');
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            case mode.update:
                ajax.put(baseUrl, ko.toJSON(self.NewCustomer()))
                    .done(function (result) {
                        self.CustomerList.replace(self.SelectedCustomer(), new customermodel(result));
                        self.resetForm();
                        //self.getData();
                        //self.resetForm();
                        $('#customerModal').modal('hide');
                    })
                    .fail(function (err) {
                        console.log(err);
                    });
                break;
            default:
                console.log("Invalid mode");
        }
    };


    self.DeleteCustomer = function (model) {
        self.customerToDelete(model);
        setTimeout(function () {
            $('#deleteConfirmModal').modal('show');
        }, 100);
    };

    self.confirmDelete = function ()
    {
        var model = self.customerToDelete();
        if (model) {
            ajax.delete(baseUrl + "?id=" + model.id())
                .done((result) => {
                    self.CustomerList.remove(model);
                    $('#deleteConfirmModal').modal('hide');
                })
                .fail((err) => {
                    console.log(err);
                    $('#deleteConfirmModal').modal('hide');
                });
        }
    };

    //self.DeleteCustomer = function (model) {
    //    if (confirm("Are you sure want to delete the customer " + model.fullName() + "?")) {
    //        ajax.delete(baseUrl + "?id=" + model.id())
    //            .done((result) => {
    //                self.CustomerList.remove(model);
    //            })
    //            .fail((err) => {
    //                console.log(err);
    //            });
    //    }
    //};


    self.SelectCustomer = (model) => {
        self.SelectedCustomer(model);
        self.NewCustomer(new customermodel(ko.toJS(model)));
        self.IsUpdated(true);
        self.mode(mode.update);
        $('#customerModal').modal('show');
    }

    self.setCreateMode = function () {
        self.resetForm();
        $('#customerModal').modal('show');
    };

    self.resetForm = () => {
        self.NewCustomer(new customermodel());
        self.IsUpdated(false);
        self.mode(mode.create);
    };

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