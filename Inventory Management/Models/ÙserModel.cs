using System.ComponentModel.DataAnnotations;

namespace Inventory_Management.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]

        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password {  get; set; }
    }
}
