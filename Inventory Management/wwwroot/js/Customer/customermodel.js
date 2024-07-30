    

var customermodel = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.fullName = ko.observable(item.fullName || '').extend({
        required: { message: "Customer Name is Required." }
    });
    self.contactNo = ko.observable(item.contactNo || '').extend({
        required: { message: "Contact No is Required." },
        maxLength: { params: 10, message: "Contact No must not exceed 10 characters." },
        minLength: { params: 10, message: "Contact No must be 10 characters long." }
    });
    self.address = ko.observable(item.address || '').extend({
        required: { message: "Customer Address is Required." }
    });

    self.errors = ko.validation.group(self);

    self.isValid = ko.computed(function () {
        return self.errors().length === 0;
    });
}