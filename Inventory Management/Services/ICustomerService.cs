using Inventory_Management.Models;

namespace Inventory_Management.Services
{
    public interface ICustomerService
    {
        List<CustomerModel> GetAll();

        CustomerModel GetById(int id);

        bool Add(CustomerModel obj);

        bool Updates(CustomerModel obj);

        int Delete(int id);
    }
}
