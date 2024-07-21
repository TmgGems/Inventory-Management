using Inventory.Entities;
using Inventory.Services.PurchaseMasterDetail;
using Inventory_Management.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentItemsInfoAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CurrentItemsInfoAPIController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<ItemCurrentInfo> GetItems()
        {
            var existingitemsdata = _context.Items.ToList();
            var existingcurrentitems = _context.ItemsCurrentInfo.ToList();

            var requireditems = (from items in existingitemsdata
                                 join item in existingcurrentitems on items.Id equals item.ItemId
                                 select new ItemCurrentInfo
                                 {
                                     Id = item.Id,
                                     ItemId = item.ItemId,
                                     ItemName = items.Name,
                                     quantity = item.quantity
                                 }).ToList();
            return requireditems;
        }
    }
}
