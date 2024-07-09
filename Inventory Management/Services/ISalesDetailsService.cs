using Inventory_Management.Models.ViewModels;

namespace Inventory_Management.Services
{
    public interface ISalesDetailsService
    {
        List<SalesMasterVM> GetAll();

        SalesMasterVM GetById(int id);

        bool Add(SalesMasterVM model);

        bool Updates(SalesMasterVM obj);

        int Delete(int id);

        IEnumerable<GetCustomersNameVM> GetCustomersName();

        IEnumerable<GetItemsNameVM> GetItemsName();
    }
}
