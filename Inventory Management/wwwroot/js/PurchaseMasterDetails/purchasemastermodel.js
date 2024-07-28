/// <reference path="../knockout.js" />

var masterpurchaseVM = function (item)
{
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.vendorId = ko.observable(item.vendorId || 0);
    self.vendorName = ko.observable(item.vendorName || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.billAmount = ko.observable(item.billAmount || 0);
    self.discount = ko.observable(item.discount || 0);
    self.netAmount = ko.observable(item.netAmount || 0);

    self.purchaseDetails = ko.observableArray((item.purchaseDetails || []).map(function (detail) {
        return new detailpurchaseVM(detail);
    }));


    // Compute bill amount whenever purchase details change
    self.purchaseDetails.subscribe(function () {
        self.updateBillAmount();
    });

    // Update bill amount and net amount
    self.updateBillAmount = function () {
        var total = self.purchaseDetails().reduce(function (sum, item) {
            return sum + item.amount();
        }, 0);
        self.billAmount(total);
        self.updateNetAmount();
    };

    // Update net amount when bill amount or discount changes
    self.updateNetAmount = function () {
        var net = self.billAmount() - self.discount();
        self.netAmount(net);
    };

    // Subscribe to discount changes
    self.discount.subscribe(self.updateNetAmount);
}

var detailpurchaseVM = function (item) {
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.unit = ko.observable(item.unit || '');
    self.quantity = ko.observable(item.quantity || 0);
    self.price = ko.observable(item.price || 0); // Ensure price is initialized
    self.amount = ko.computed(function () {
        return (parseFloat(self.quantity()) || 0) * (parseFloat(self.price()) || 0);
    });

    self.quantity.subscribe(function () {
        self.amount.notifySubscribers();
    });
    self.price.subscribe(function () {
        self.amount.notifySubscribers();
    });
}
var detailpurchaseVM = function (item) {
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.unit = ko.observable(item.unit || '');
    self.quantity = ko.observable(item.quantity || 0);
    self.price = ko.observable(item.price || 0); // Ensure price is initialized
    self.amount = ko.computed(function () {
        return (parseFloat(self.quantity()) || 0) * (parseFloat(self.price()) || 0);
    });

    self.quantity.subscribe(function () {
        self.amount.notifySubscribers();
    });
    self.price.subscribe(function () {
        self.amount.notifySubscribers();
    });
}


var itemnamemodel = function (item)
{
    var self = this;
    item = item || {};
    self.itemId = ko.observable(item.itemId || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.itemUnit = ko.observable(item.itemUnit || '');
}

var vendornamemodel = function (item)
{
    var self = this;
    item = item || {};
    self.vendorId = ko.observable(item.vendorId || 0);
    self.vendorName = ko.observable(item.vendorName || '');
}


var purchaseReportmodel = function (item)
{
    var self = this;
    item = item || {};
    self.date = ko.observable(item.date || '');
    self.vendorName = ko.observable(item.vendorName || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.itemName = ko.observable(item.itemName || '');
    self.quantityPurchased = ko.observable(item.quantityPurchased || 0);
    self.unitPrice = ko.observable(item.unitPrice || 0);
    self.totalPurchaseAmount = ko.observable(item.totalPurchaseAmount || 0);
}
 