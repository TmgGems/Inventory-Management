using Inventory.Entities;
using Inventory_Management.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]/[Action]")]
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

        [HttpGet]
        public List<ItemCurrentInfoHistory> SearchItemName(string searchedItem)
        {
            var itemid = _context.Items
                            .Where(x => x.Name.ToLower() == searchedItem.ToLower())
                            .Select(i => i.Id)
                            .ToList();

            var historydata = _context.ItemsHistoryInfo
                .Where(x => itemid.Contains(x.ItemId)).ToList();

            var items = _context.Items.ToList();
            var result = (from infoHistory in historydata
                          join item in items on infoHistory.ItemId equals item.Id
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



        [HttpGet]
        public List<ItemCurrentInfoHistory> SearchTransType(string transType)
        {
            // Convert the string transType to the enum TransactionType
            if (Enum.TryParse(transType, true, out TransactionType transactionType))
            {
                var historydata = _context.ItemsHistoryInfo
               .Where(x => x.TransactionType == transactionType)
               .ToList();

                var items = _context.Items.ToList();
                var result = (from infoHistory in historydata
                              join item in items on infoHistory.ItemId equals item.Id
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
            else
            {
                // If conversion fails, return an empty list or handle the error as needed
                return new List<ItemCurrentInfoHistory>();
            }
        }


        [HttpGet]
        public List<ItemCurrentInfoHistory> Search(string searchedItem, string transType)
        {
            IQueryable<ItemCurrentInfoHistory> query = _context.ItemsHistoryInfo;

            // If searchedItem is provided, filter by item name
            if (!string.IsNullOrEmpty(searchedItem))
            {
                var itemIds = _context.Items
                    .Where(x => x.Name.ToLower() == searchedItem.ToLower())
                    .Select(i => i.Id)
                    .ToList();

                query = query.Where(x => itemIds.Contains(x.ItemId));
            }

            // If transType is provided, filter by transaction type
            if (!string.IsNullOrEmpty(transType) && Enum.TryParse(transType, true, out TransactionType transactionType))
            {
                query = query.Where(x => x.TransactionType == transactionType);
            }

            // Execute the query and join with items to get item names
            var historydata = query.ToList();
            var items = _context.Items.ToList();
            var result = (from infoHistory in historydata
                          join item in items on infoHistory.ItemId equals item.Id
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

