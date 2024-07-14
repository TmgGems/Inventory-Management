using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inventory_Management.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Unit {  get; set; }

        public string Category {  get; set; }

       
    }
}
