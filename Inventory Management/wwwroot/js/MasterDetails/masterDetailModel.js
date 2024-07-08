/// <reference path="../knockout.js" />

var mastermodelVM = function (item)
{
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.salesDate = ko.observable(item.salesDate || '');
    self.customerId = ko.observable(item.customerId || '');
    self.invoiceNumber = ko.observable(item.invoiceNumber || 0);
    self.customerName = ko.observable(item.customerName || '');
    self.billAmount = ko.observable(item.billAmount || 0);
    self.discount = ko.observable(item.discount || 0);
    self.netAmount = ko.observable(item.netAmount || 0);
    self.sales = ko.observable((item.sales || []).map(function (item) {
        return new detailsmodelVM(item);
    }));
}

var detailsmodelVM = function (item)
{
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.itemId = ko.observable(item.itemId || 0);
    self.unit = ko.observable(item.unit || '');
    self.quantity = ko.observable(item.quantity || 0);
    self.price = ko.observable(item.price || 0);
    self.amount = ko.observable(item.amount || 0);
}