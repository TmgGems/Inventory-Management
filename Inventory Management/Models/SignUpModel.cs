using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Models
{
    public class SignUpModel
    {
        
        public string Username {  get; set; }
        [EmailAddress]
        public string Email {  get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
