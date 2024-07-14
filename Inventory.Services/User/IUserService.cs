using Inventory_Management.Models;

namespace Inventory.Services.User
{
    public interface IUserService
    {
        // bool ValidateLogin(string userName, string password);

        bool RegisterUser(UserModel model);

        UserModel GetUserWithRole(string userName, string password);
    }
}
