using Inventory_Management.Models;

namespace Inventory.Services.Item
{
    public interface IItemService
    {
        List<ItemModel> GetAll();

        ItemModel GetById(int id);

        bool Add(ItemModel obj);

        bool Updates(ItemModel obj);

        int Delete(int id);

        List<ItemModel> SearchItemName(string searchTerm);
    }
}
