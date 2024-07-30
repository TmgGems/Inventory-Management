/// <reference path="../knockout.js" />

var purchasemasterdetailcontroller = function () {
    var self = this;
    const baseUrl = "/api/PurchaseMasterDetailsAPI";
    self.PurchaseMasterDetailList = ko.observableArray([]);
    self.VendorsNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.selectedPurchase = ko.observableArray([]);
    self.NewPurhaseOrder = ko.observable(new masterpurchaseVM());
    self.IsUpdated = ko.observable(false);
    self.ReportData = ko.observableArray([]);
    self.showReport = ko.observable(false);

    self.getData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.PurchaseMasterDetailList(result.map(item => new masterpurchaseVM(item)));
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

    self.
    self.getReportData = function () {
        console.log("Report Data Called");
        ajax.get(baseUrl + "/GetReport").then(function (result) {
            console.log("Report Data Received", result);
            self.ReportData(result.map(item => new purchaseReportmodel(item)));
            self.showReport(true);
            console.log("Show Report Set To True");
        });

    }

    self.backToMain = function () {
        self.showReport(false);
    }
    self.AddPurchase = function () {
        var purchaseData = ko.toJS(self.NewPurhaseOrder());
        if (!purchaseData.purchaseDetails || purchaseData.purchaseDetails === 0) {
            alert("Add at least one item .");
            return;
        }
        if (self.IsUpdated()) {
         
            ajax.put(baseUrl + "/Updates", JSON.stringify(purchaseData))
                .done(function (result) {
                    var updatedPurchase = new masterpurchaseVM(result);
                    var index = self.PurchaseMasterDetailList().findIndex(function (item) {
                        return item.id() == updatedPurchase.id();
                    });
                    if (index >= 0) {
                        self.PurchaseMasterDetailList.replace(self.PurchaseMasterDetailList()[index], updatedPurchase);
                    }
                    self.resetForm();
                    self.getData();
                    $('#purchaseModal').modal('hide');
                })
                .fail(function (err) {
                    console.error("Error updating Purchases:", err);
                });
        }
        else
        {
            ajax.post(baseUrl + "/Add", ko.toJSON(self.NewPurhaseOrder()))
                .done(function (result) {
                    self.PurchaseMasterDetailList.push(new masterpurchaseVM(result));
                    self.resetForm();
                    self.getData();
                    $('#purchaseModal').modal('hide');
                })
                .fail(function (err) {
                    console.log(err);
                });
        }
    }

    self.DeletePurchase = function (model) {
        ajax.delete(baseUrl + "/Delete?id=" + model.id())
            .done(function (result) {
                self.PurchaseMasterDetailList.remove(function (item) {
                    return item.id() === model.id();
                });
            }).fail(function (err) {
                console.error("Error deleting sale:", err);
            });
    };


    self.SelectVendor = function (model) {
        var purchasedata = ko.toJS(model);
        var newPurchaseData = new masterpurchaseVM(purchasedata);
        newPurchaseData.purchaseDetails(purchasedata.purchaseDetails.map(function (detail) {
            var detailVM = new detailpurchaseVM(detail);
            return detailVM;
        }));

        self.NewPurhaseOrder(newPurchaseData);
        self.IsUpdated(true);
        $('#purchaseModal').modal('show');
        ko.tasks.runEarly();
    }

    self.getVendorsName = function () {
        var url = baseUrl + "/GetVendorsName";
        ajax.get(url).then(function (result) {
            self.VendorsNameList(result.map(item => new vendornamemodel(item)));
        });
    }

    self.getVendorsName();

    self.getItemsName = function () {
        var url = baseUrl + "/GetItemsName";
        ajax.get(url).then(function (result) {
            self.ItemsNameList(result.map(item => new itemnamemodel(item)));
        });
    }

    self.getItemsName();


    self.AddItem = function () {

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

    self.removeItem = function (item) {
        self.NewPurhaseOrder().purchaseDetails.remove(item);
    }
    self.recalculateAmounts = function () {
        self.NewPurhaseOrder().purchaseDetails().forEach(function (detail) {
            detail.amount(detail.quantity() * detail.price());
        });
        self.NewPurhaseOrder().updateBillAmount();
    };
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