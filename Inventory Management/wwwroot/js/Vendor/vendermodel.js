/// <reference path="../knockout.js" />

var VendorModel = function (item) {
    var self = this;
    item = item || {};
    self.Id = ko.observable(item.id || 0);
    self.Name = ko.observable(item.name || '').extend({
        required: { message: "Name Field is Required." }
    });
    self.Contact = ko.observable(item.contact || '').extend({
        required: { message: "Contact No is Required." },
        maxLength: { params: 10, message: "Contact No must not exceed 10 characters." },
        minLength: { params: 10, message: "Contact No must be 10 characters long." }
       
    });
    self.Address = ko.observable(item.address || '').extend({
        required: { message: "Vendor Address is Required." }
    });

    self.errors = ko.validation.group(self);
    self.isValid = ko.computed(function ()
    {
        return self.errors().length == 0;
    });
}