using Inventory_Management.Data;
using Inventory_Management.Models;

namespace Inventory.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;
        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(ItemModel obj)
        {
            _context.Items.Add(obj);
            _context.SaveChanges();
            return true;
        }

        public int Delete(int id)
        {
            var data = _context.Items.Find(id);
            if (data != null)
            {
                _context.Items.Remove(data);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public List<ItemModel> GetAll()
        {
            var items = _context.Items.ToList();
            return items;
        }

        public ItemModel GetById(int id)
        {
            var itemData = _context.Items.Find(id);
            if (itemData != null)
            {
                return itemData;
            }
            return null;
        }

        public bool Updates(ItemModel obj)
        {
            var itemdata = _context.Items.Find(obj.Id);
            if (itemdata != null)
            {
                itemdata.Name = obj.Name;
                itemdata.Unit = obj.Unit;
                itemdata.Category = obj.Category;
                _context.Items.Update(itemdata);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
