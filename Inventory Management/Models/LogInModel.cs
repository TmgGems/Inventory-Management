using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Models
{
    public class LogInModel
    {
        public int Id { get; set; }

        [EmailAddress]
        [DisplayName("Email")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string PassWord {  get; set; }
    }
}
