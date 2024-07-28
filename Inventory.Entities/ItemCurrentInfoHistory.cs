using Inventory_Management.Models;
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
    public class ItemCurrentInfoHistory
    {
        [Key]
        public int Id {  get; set; }

        [ForeignKey("ItemId")]
        public int ItemId {  get; set; }
        [JsonIgnore]
        public ItemModel Item { get; set; }

        [NotMapped]
        public string ItemName { get; set; }

        public int Quantity {  get; set; }
        public DateTime TransDate { get; set; }

        public StockCheckOut StockCheckOut {  get; set; }

        public TransactionType TransactionType {  get; set; }




        [NotMapped]
        public string TransDateFormatted => TransDate.ToString("yyyy-MM-dd");

        [NotMapped]
        public string StockCheckOutText => StockCheckOut.ToString();

        [NotMapped]
        public string TransactionTypeText => TransactionType.ToString();
    }

    public enum StockCheckOut
    {
        In,
        Out,
        Previous,
        Now
    }

    public enum TransactionType
    {
        Purchase,
        Sales
    }
}
