using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory.Entities
{
    public class PurchaseMasterModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        [ForeignKey("VendorId")]
        public int VendorId { get; set; }
        [JsonIgnore]
        public VendorModel Vendor { get; set; }

        public int InvoiceNumber { get; set; }

        public decimal BillAmount {  get; set; }

        public decimal Discount {  get; set; }

        public decimal NetAmount {  get; set; }

    }
}
