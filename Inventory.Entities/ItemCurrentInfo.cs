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
    public class ItemCurrentInfo
    {
        [Key]
        public int Id {  get; set; }

        [ForeignKey("ItemId")]
        
        public int ItemId { get; set; }
        [JsonIgnore]
        public ItemModel Item {  get; set; }


        [NotMapped]
        public string ItemName { get; set; }

        public int quantity {  get; set; }
    }
}
