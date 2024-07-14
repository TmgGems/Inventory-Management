using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Full Name is required .")]
        public string FullName { get; set; }

        [Required (ErrorMessage ="Conact No is required .")]
        public string ContactNo { get; set; }

        [Required (ErrorMessage ="Address is required .")]
        public string Address { get; set; }
    }
}
