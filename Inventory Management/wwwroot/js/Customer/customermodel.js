var customermodel = function(item)
{
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.fullName = ko.observable(item.fullName || '');
    self.contactNo = ko.observable(item.contactNo || '');
    self.address = ko.observable(item.address || '');
}