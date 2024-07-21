/// <reference path="../knockout.js" />

var purchasemasterdetailcontroller = function ()
{
    var self = this;
    const baseUrl = "/api/PurchaseMasterDetailsAPI";
    self.PurchaseMasterDetailList = ko.observableArray([]);
    self.VendorsNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.selectedPurchase = ko.observableArray([]);
    self.NewPurhaseOrder = ko.observable(new masterpurchaseVM());
    self.IsUpdated = ko.observable(false);

    self.getData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.PurchaseMasterDetailList(result.map(item => new masterpurchaseVM(item)));
        });
    }
    self.getData();


    self.SelectVendor = function (model)
    {
        self.selectedPurchase(model);
       // self.NewPurhaseOrder(new masterpurchaseVM(ko.toJS(model)));
        self.IsUpdated(true);
        $('purchaseModal').modal('show');
    }
    self.getVendorsName = function ()
    {
        var url = baseUrl + "/GetVendorsName";
        ajax.get(url).then(function (result) {
            self.VendorsNameList(result.map(item => new vendornamemodel(item)));
        });
    }

    self.getVendorsName();

    self.getItemsName = function ()
    {
        var url = baseUrl + "/GetItemsName";
        ajax.get(url).then(function (result) {
            self.ItemsNameList(result.map(item => new itemnamemodel(item)));
        });
    }

    self.getItemsName();


    self.AddItem = function ()
    {
        self.NewPurhaseOrder().purchaseDetails.push(new detailpurchaseVM());
    }

    self.updateUnit = function (item, event) {
        var selectedItemId = event.target.value;
        console.log('Selected Item ID:', selectedItemId);

        var selectedItem = ko.utils.arrayFirst(self.ItemsNameList(), function (item) {
            return item.itemId() == selectedItemId;
        });

        console.log('Selected Item:', selectedItem);

        if (selectedItem) {
            console.log('Item Unit:', selectedItem.itemUnit());
            item.unit(selectedItem.itemUnit());
        } else {
            console.log('No item found');
            item.unit('');
        }
    };

    self.removeItem = function (item)
    {
        self.NewPurhaseOrder().purchaseDetails.remove(item);
    }

    self.setCreateMode = function () {
        self.resetForm();
        $('#purchaseModal').modal('show');
    };

    self.resetForm = () => {
        self.NewPurhaseOrder(new masterpurchaseVM());
        self.IsUpdated(false);
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