using Inventory.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory_Management.Models;

namespace Inventory.Services.PurchaseMasterDetail.ViewModel
{
    public class PurchaseMasterVM
    {

        [Key]
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }
        public int VendorId { get; set; }

        public string ? VendorName {  get; set; }    
        public int InvoiceNumber { get; set; }

        public decimal BillAmount { get; set; }

        public decimal Discount { get; set; }

        public decimal NetAmount { get; set; }

        public List<PurchaseDetailVM> PurchaseDetails {  get; set; }

    }

    public class PurchaseDetailVM
    {
       
        public int Id { get; set; }
       
        public int ItemId { get; set; }

        public string ? ItemName {  get; set; }
        
        public string Unit { get; set; }

        public int Quantity { get; set; }

        public decimal Price {  get; set; }

        public decimal Amount { get; set; }
    }


}
