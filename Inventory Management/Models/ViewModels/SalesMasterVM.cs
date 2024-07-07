using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventory_Management.Models.ViewModels
{
    public class SalesMasterVM
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }

        public int CustomerId { get; set; }

        public int InvoiceNumber { get; set; }

        public decimal BillAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal NetAmount { get; set; }

        public List<SalesDetailsVM> Sales {  get; set; }
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
