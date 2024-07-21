using Inventory.Entities;
using Inventory_Management.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsInfoHistoryAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemsInfoHistoryAPIController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public List<ItemCurrentInfoHistory> GetAll()
        {
            // Fetch all data from the ItemCurrentInfoHistory table
            var infoHistoryData = _context.ItemsHistoryInfo.ToList();

            // Fetch all data from the ItemModel table
            var itemData = _context.Items.ToList();

            // Join the two lists based on ItemId and populate ItemName
            var result = (from infoHistory in infoHistoryData
                          join item in itemData on infoHistory.ItemId equals item.Id
                          select new ItemCurrentInfoHistory
                          {
                              Id = infoHistory.Id,
                              ItemId = infoHistory.ItemId,
                              Item = item,
                              ItemName = item.Name,
                              Quantity = infoHistory.Quantity,
                              TransDate = infoHistory.TransDate,
                              StockCheckOut = infoHistory.StockCheckOut,
                              TransactionType = infoHistory.TransactionType
                          }).ToList();

            return result;
        }

    }
}
