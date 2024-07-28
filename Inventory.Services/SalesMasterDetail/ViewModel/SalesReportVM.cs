using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.SalesMasterDetail.ViewModel
{
    public class SalesReportVM
    {
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public int InvoiceNumber { get; set; }
        public string ItemName { get; set; }
        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalSalesAmount { get; set; }
    }
}
