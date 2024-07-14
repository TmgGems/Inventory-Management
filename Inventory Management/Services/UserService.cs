using Inventory_Management.Data;
using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool RegisterUser(UserModel model)
        {
            bool alreadyExist = _context.UerModel.Count(x => x.Username == model.Username) > 0;
            if (!alreadyExist)
            {
                _context.UerModel.Add(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //public bool ValidateLogin(string userName, string password)
        //{
        //    bool validUser = _context.UerModel.Any(x => x.Username == userName && x.Password == password);
        //    return validUser;
        //}

        public UserModel GetUserWithRole(string userName, string password)
        {
            return _context.UerModel.FirstOrDefault(x => x.Username == userName && x.Password == password);
        }
    }
}
