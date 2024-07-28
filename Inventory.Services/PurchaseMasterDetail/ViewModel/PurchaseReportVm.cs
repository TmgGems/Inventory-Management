using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.PurchaseMasterDetail.ViewModel
{
    public  class PurchaseReportVm
    {
        public string Date { get; set; }

        public string VendorName {  get; set; }

        public int InvoiceNumber {  get; set; }

        public string ItemName {  get; set; }

        public int QuantityPurchased {  get; set; }

        public decimal UnitPrice {  get; set; }

        public decimal TotalPurchaseAmount {  get; set; }
    }
}
