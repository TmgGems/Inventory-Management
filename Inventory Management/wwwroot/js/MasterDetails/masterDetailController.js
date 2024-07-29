/// <reference path="../knockout.js" />


const mode = {
    create: 1,
    update: 2
};
var masterdetailsController = function () {
    var self = this;
    const baseUrl = "/api/SalesDetailsAPI";
    self.SalesList = ko.observableArray([]);
    self.CustomersNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.NewSales = ko.observable(new mastermodelVM({}, self));
    self.SelectedSales = ko.observable(new mastermodelVM());
    self.IsUpdated = ko.observable(false);
    self.SalesReportList = ko.observableArray([]);
    self.showReport = ko.observable(false);
    //self.NewSales().sales.push(new detailsmodelVM());
    //Get All Data
    self.getData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.SalesList(result.map(item => new mastermodelVM(item, self)));
        });
    }
    self.getData();


    self.downloadPDF = function () {
        var doc = new jspdf.jsPDF();
        doc.autoTable({ html: '#reportTable' });
        doc.save('purchase_report.pdf');
    };

    self.downloadExcel = function () {
        var wb = XLSX.utils.table_to_book(document.getElementById('reportTable'), { sheet: "Purchase Report" });
        XLSX.writeFile(wb, 'purchase_report.xlsx');
    };

    // Get CustomerNames
    self.getSalesReport = function()
    {
        ajax.get(baseUrl + "/GetSalesReport").then(function (result)
        {
            self.SalesReportList(result.map(item => new salesreportmodel(item, self)));
        self.showReport(true);
        });
    }

    self.getCustomersName = function () {
        var url = baseUrl + "/GetCustomersName";
        console.log("Fetching products from URL: " + url);

        return ajax.get(url).then(function (data) {
            // console.log("Products received: ", data);
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new customernamemodel(item);
            });
            self.CustomersNameList(mappedProducts);
            console.log("Customers Data: ", self.CustomersNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching customersname: ", textStatus, errorThrown);
        });
    };

    self.getCustomersName();

    //Get ItemNames
    self.getItemsName = function () {
        var url = baseUrl + "/GetItemsName";
        console.log("Fetching products from URL: " + url);

        return ajax.get(url).then(function (data) {
            // console.log("Products received: ", data);
            var mappedProducts = ko.utils.arrayMap(data, (item) => {
                return new itemnamemodel(item);
            });
            self.ItemsNameList(mappedProducts);
            console.log("Items Data: ", self.ItemsNameList());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching itemsname: ", textStatus, errorThrown);
        });
    };

    self.getItemsName();

    self.AddSales = function () {
        var salesData = ko.toJS(self.NewSales());
        if (!salesData.sales || salesData.sales.length === 0) {
            alert("Add at least one item.");
            return;
        }

        if (self.isNetAmountNegative()) {
            alert("Net amount cannot be negative. Please adjust the discount or items.");
            return;
        }

        if (self.IsUpdated()) {
            ajax.put(baseUrl + "/Updates", JSON.stringify(salesData))
                .done(function (result) {
                    if (result.success) {
                        var updatedSales = new mastermodelVM(result);
                        var index = self.SalesList().findIndex(function (item) {
                            return item.id() === updatedSales.id();
                        });
                        if (index >= 0) {
                            self.SalesList.replace(self.SalesList()[index], updatedSales);
                        }
                        self.resetForm();
                        self.getData();
                        $('#salesModal').modal('hide');
                        alert(result.message); // Display success message
                    } else {
                        alert(result.message || "Failed to update sales."); // Display error message
                    }
                })
                .fail(function (err) {
                    console.error("Error updating sales:", err);
                    alert("Failed to update sales: " + (err.responseJSON ? err.responseJSON.message : err.statusText));
                });
        } 
        else {

            ajax.post(baseUrl + "/Add", ko.toJSON(self.NewSales()))
                .done(function (result) {
                    if (result.success) {
                        self.SalesList.push(new mastermodelVM(result));
                        self.resetForm();
                        self.getData();
                        $('#salesModal').modal('hide');
                        alert(result.message); // Display success message
                    } else {
                        alert(result.message || "Failed to add sales."); // Display error message
                    }
                })
                .fail(function (err) {
                    console.log(err);
                    alert("Failed to add sales: " + (err.responseJSON ? err.responseJSON.message : err.statusText));
                });
        }
    };


    //self.AddItem = function () {
    //    switch (self.mode()) {
    //        case mode.create:
    //            ajax.post(baseUrl, ko.toJSON(self.NewItem()))
    //                .done(function (result) {
    //                    if (result.success) {
    //                        console.log(result.success);
    //                        self.ItemList.push(new itemModel(result.data));
    //                        self.resetForm();
    //                        self.getData();
    //                        $('#itemModal').modal('hide');
    //                    } else {
    //                        alert(result.message || "An error occurred.");
    //                    }
    //                })
    //                .fail(function (err) {
    //                    console.log(err);
    //                    alert("Failed to add item: " + (err.responseJSON ? err.responseJSON.message : err.statusText));
    //                });
    //            break;
    //        case mode.update:
    //            ajax.put(baseUrl, ko.toJSON(self.NewItem()))
    //                .done(function (result) {
    //                    if (result.success) {
    //                        self.ItemList.replace(self.selectedList(), new itemModel(result.data));
    //                        self.resetForm();
    //                        self.getData();
    //                        $('#itemModal').modal('hide');
    //                    } else {
    //                        alert(result.message || "An error occurred.");
    //                    }
    //                })
    //                .fail(function (err) {
    //                    console.log(err);
    //                    alert("Failed to update item: " + (err.responseJSON ? err.responseJSON.message : err.statusText));
    //                });
    //            break;
    //        default:
    //            console.log("Invalid mode");
    //    }
    //};


    self.DeleteSales = function (model) {
        ajax.delete(baseUrl + "/Delete?id=" + model.id())
            .done(function (result) {
                self.SalesList.remove(function (item) {
                    return item.id() === model.id();
                });
            }).fail(function (err) {
                console.error("Error deleting sale:", err);
            });
    };

    self.isNetAmountNegative = function () {
        var netAmount = self.NewSales().netAmount();
        return self.NewSales().netAmount() < 0;
    };

    self.SelectSale = function (model) {
        var clonedModel = ko.toJS(model);
        if (clonedModel.salesDate) {
            clonedModel.salesDate = new Date(clonedModel.salesDate).toISOString().slice(0, 16);
        }
        self.NewSales(new mastermodelVM(clonedModel, self));
        self.IsUpdated(true);
        $('#salesModal').modal('show');
    }


    self.resetForm = () => {
        self.NewSales(new mastermodelVM({}, self));
        self.IsUpdated(false);
    };

    self.setCreateMode = function () {
        self.resetForm();
        $('#salesModal').modal('show');
    };
    self.AddItem = function () {
        self.NewSales().sales.push(new detailsmodelVM({}, self));
    };
    self.removeItem = function (item) {
        self.NewSales().sales.remove(item);
    };
};



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