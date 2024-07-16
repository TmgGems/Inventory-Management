using Inventory_Management.Data;
using Inventory_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services.Item
{
    public class ItemResult
    {
        public bool Success { get; set; }
        public ItemModel Data { get; set; }
    }
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;
        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        //public bool Add(ItemModel obj)
        //{

        //   var existingItem = _context.Items.FirstOrDefault( x => x.Name.ToLower() == obj.Name.ToLower() );
        //    if(existingItem != null)
        //    {
        //        return false;
        //    }
        //    _context.Items.Add(obj);
        //    _context.SaveChanges();
        //    return true;
        //}

        public ItemResult Add(ItemModel obj)
        {
            var existingItem = _context.Items.FirstOrDefault(x => x.Name.ToLower() == obj.Name.ToLower());
            if (existingItem != null)
            {
                return new ItemResult { Success = false };
            }
            _context.Items.Add(obj);
            _context.SaveChanges();
            return new ItemResult { Success = true, Data = obj };
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



        public List<ItemModel> SearchItemName(string searchTerm)
        {
            var data = _context.Items.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            return data;
        }

        public ItemResult Updates(ItemModel obj)
        {
            var itemdata = _context.Items.Find(obj.Id);
            bool existingItem = _context.Items.Any(x => x.Name.ToLower() == obj.Name.ToLower() && x.Id != obj.Id);
            if (existingItem )
            {
                return new ItemResult { Success = false };
            }
            itemdata.Name = obj.Name;
            itemdata.Unit = obj.Unit;
            itemdata.Category = obj.Category;
            _context.Items.Update(itemdata);
            _context.SaveChanges();
            return new ItemResult { Success = true, Data = obj };
        }
    }
}
