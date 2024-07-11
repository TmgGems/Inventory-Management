using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    public interface IUserService
    {
        bool ValidateLogin(string userName, string password);

        bool RegisterUser(UserModel model);

        UserModel GetUserWithRole(string userName, string password);
    }
}
