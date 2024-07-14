//var customermodel = function(item)
//{
//    var self = this;
//    item = item || {};
//    self.id = ko.observable(item.id || 0);
//    self.fullName = ko.observable(item.fullName || '').extend({
//        validation: {
//            message:"Full Name is Required ."
//        }
//    });
//    self.contactNo = ko.observable(item.contactNo || '');
//    self.address = ko.observable(item.address || '');

//    //self.fullName = ko.observable(item.fullName || '').extend({
//    //    required: { message: 'Full Name is required.' }
//    //});
//    //self.contactNo = ko.observable(item.contactNo || '').extend({
//    //    required: { message: 'Contact No is required.' }
//    //});
//    //self.address = ko.observable(item.address || '').extend({
//    //    required: { message: 'Address is required.' }
//    //});

//    //self.errors = ko.validation.group(self);
//}

var customermodel = function (item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.fullName = ko.observable(item.fullName || '').extend({
        required: { message: "Full Name is Required." }
    });
    self.contactNo = ko.observable(item.contactNo || '').extend({
        required: { message: "Contact No is Required." }
    });
    self.address = ko.observable(item.address || '').extend({
        required: { message: "Address is Required." }
    });

    self.errors = ko.validation.group(self);

    self.isValid = ko.computed(function () {
        return self.errors().length === 0;
    });
}