/// <reference path="../knockout.js" />

var mastermodelVM = function (item, parent) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.salesDate = ko.observable(item.salesDate || '');
    self.customerId = ko.observable(item.customerId || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.customerName = ko.observable(item.customerName || '');
    self.discount = ko.observable(item.discount || 0);
    self.sales = ko.observableArray((item.sales || []).map(function (saleItem) {
        return new detailsmodelVM(saleItem, parent);
    }));

    self.billAmount = ko.computed(function () {
        return self.sales().reduce((sum, item) => sum + item.amount(), 0);
    });

    self.netAmount = ko.computed(function () {
        return self.billAmount() - parseFloat(self.discount() || 0);
    });
}

var detailsmodelVM = function (item, parent) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.itemId = ko.observable(item.itemId || 0);
    self.unit = ko.observable(item.unit || '');
    self.quantity = ko.observable(item.quantity || 0);
    self.price = ko.observable(item.price || 0);
    self.availableQuantity = ko.observable(item.availableQuantity || 0); 
    self.selectedItem = ko.computed(function () {
        return parent.ItemsNameList().find(function (listItem) {
            return listItem.itemId() == self.itemId();
        });
    });

    self.itemId.subscribe(function (newValue) {
        var selected = self.selectedItem();
        if (selected) {
            self.unit(selected.itemUnit());
            self.availableQuantity(selected.availableQuantity());
        }
    });

    self.amount = ko.computed(function () {
        return self.quantity() * self.price();
    });
}

var customernamemodel = function (item) {
    var self = this;
    item = item || {};
    self.customerId = ko.observable(item.customerId || 0);
    self.customerName = ko.observable(item.customerName || '');
}

var itemnamemodel = function (item) {
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.itemUnit = ko.observable(item.itemUnit || '');
    self.availableQuantity = ko.observable(item.quantity || 0);
}

var salesreportmodel = function (item) {
    var self = this;
    item = item || {};
    self.date = ko.observable(item.date || '');
    self.customerName = ko.observable(item.customerName || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.quantitySold = ko.observable(item.quantitySold || 0);
    self.unitPrice = ko.observable(item.unitPrice || 0);
    self.totalSalesAmount = ko.observable(item.totalSalesAmount || 0);
}