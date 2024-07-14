using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventory_Management.Models
{
    public class SalesDetailsModel
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        
        public int ItemId {  get; set; }
        [JsonIgnore]
        public ItemModel Item { get; set; }

        public string Unit {  get; set; }

        public int quantity { get; set; }

        public decimal price { get; set; }

        public decimal Amount {  get; set; }

        [ForeignKey("SalesMasterId")]
        public int SalesMasterId {  get; set; }
        [JsonIgnore]
        public SalesMasterModel SalesMaster { get; set; }
    }
}
