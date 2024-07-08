/// <reference path="../knockout.js" />

var itemModel = function (item)
{
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.name = ko.observable(item.name || '');
    self.unit = ko.observable(item.unit || '');
    self.category = ko.observable(item.category || '');
}