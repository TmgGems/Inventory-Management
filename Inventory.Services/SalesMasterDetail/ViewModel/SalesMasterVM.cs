using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.MasterDetail.ViewModel
{
    public class SalesMasterVM
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }
        public int InvoiceNumber { get; set; }

        public decimal BillAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal NetAmount { get; set; }

        public List<SalesDetailsVM> Sales { get; set; }
    }

    public class SalesDetailsVM
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public string Unit { get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }

        public decimal Amount { get; set; }


    }
}
