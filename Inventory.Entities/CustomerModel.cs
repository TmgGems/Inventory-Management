using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }


        public string FullName { get; set; }

        [StringLength(10)]
        public string ContactNo { get; set; }

        public string Address { get; set; }
    }
}
