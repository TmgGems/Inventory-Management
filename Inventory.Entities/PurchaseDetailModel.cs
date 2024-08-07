﻿using Inventory_Management.Models;
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
    public class PurchaseDetailModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ItemId")]
        public int ItemId {  get; set; }
        [JsonIgnore]
        public ItemModel Item { get; set; }

        public string Unit {  get; set; }

        public int Quantity {  get; set; }

        public decimal Price { get; set; }

        public decimal Amount {  get; set; }
        [ForeignKey("PurchaseMasterId")]
        public int PurchaseMasterId {  get; set; }
        [JsonIgnore]
        public PurchaseMasterModel PurchaseMaster { get; set; }
    }
}
