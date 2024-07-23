using Inventory.Entities;
using Inventory.Services.Item.ViewModel;
using Inventory.Services.MasterDetail.ViewModel;
using Inventory.Services.Modell;
using Inventory_Management.Data;
using Inventory_Management.Models;

namespace Inventory.Services.MasterDetail
{

    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class SalesDetailsService : ISalesDetailsService
    {
        private readonly ApplicationDbContext _context;
        public SalesDetailsService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetItemName(int ItemId)
        {
            var itemsdata = _context.Items.FirstOrDefault(x => x.Id == ItemId);
            return itemsdata.Name;
        }
        private bool IsItemAvailable(int itemId, int quantity)
        {
            var currentItemInfo = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == itemId);
            return currentItemInfo != null && currentItemInfo.quantity >= quantity;
        }

        public List<SalesMasterVM> GetAll()
        {
            List<SalesMasterVM> datas = new List<SalesMasterVM>();
            var masterdata = _context.SalesMaster.ToList();

            if (masterdata.Count() > 0)
            {
                foreach (var item in masterdata)
                {
                    var customer = _context.Customers.FirstOrDefault(c => c.Id == item.CustomerId);
                    var detail = _context.SalesDetails.Where(x => x.SalesMasterId == item.Id).ToList();
                    var data = new SalesMasterVM()
                    {
                        Id = item.Id,
                        SalesDate = item.SalesDate,
                        CustomerId = item.CustomerId,
                        CustomerName = customer?.FullName,
                        InvoiceNumber = item.InvoiceNumber,
                        BillAmount = item.BillAmount,
                        Discount = item.Discount,
                        NetAmount = item.NetAmount
                    };

                    data.Sales = (from d in detail
                                  select new SalesDetailsVM()
                                  {
                                      Id = d.Id,
                                      ItemId = d.ItemId,
                                      Unit = d.Unit,
                                      quantity = d.quantity,
                                      price = d.price,
                                      Amount = d.Amount
                                  }).ToList();
                    datas.Add(data);

                }
            }
            return datas;
        }
        public SalesMasterVM GetById(int id)
        {
            var masterdata = _context.SalesMaster.Find(id);
            if (masterdata == null)
            {
                return new SalesMasterVM();
            }
            else
            {
                var detail = _context.SalesDetails.Where(x => x.SalesMasterId == masterdata.Id).ToList();
                var data = new SalesMasterVM()
                {
                    Id = masterdata.Id,
                    SalesDate = masterdata.SalesDate,
                    CustomerId = masterdata.CustomerId,
                    InvoiceNumber = masterdata.InvoiceNumber,
                    BillAmount = masterdata.BillAmount,
                    Discount = masterdata.Discount,
                    NetAmount = masterdata.NetAmount
                };
                data.Sales = (from d in detail
                              select new SalesDetailsVM()
                              {
                                  Id = d.Id,
                                  ItemId = d.ItemId,
                                  Unit = d.Unit,
                                  quantity = d.quantity,
                                  price = d.price,
                                  Amount = d.Amount
                              }).ToList();
                return data;
            }
        }
        public ResponseModel Add(SalesMasterVM model)
        {
            if (model.Sales.Count == 0)
            {
                return new ResponseModel { Success = false, Message = "No items to add." };
            }

            foreach (var item in model.Sales)
            {
                if (!IsItemAvailable(item.ItemId, item.quantity))
                {
                    string itemName = GetItemName(item.ItemId);
                    return new ResponseModel { Success = false, Message = $"Item {itemName} is not available in the required quantity." };
                }
            }
            var masterData = new SalesMasterModel()
            {
                Id = 0,
                SalesDate = model.SalesDate,
                CustomerId = model.CustomerId,
                InvoiceNumber = model.InvoiceNumber,
                BillAmount = model.BillAmount,
                Discount = model.Discount,
                NetAmount = model.NetAmount
            };

            var masterdata = _context.SalesMaster.Add(masterData);
            _context.SaveChanges();

            if (masterData != null)
            {
                var details = from c in model.Sales
                              select new SalesDetailsModel
                              {
                                  Id = 0,
                                  ItemId = c.ItemId,
                                  Unit = c.Unit,
                                  quantity = c.quantity,
                                  price = c.price,
                                  Amount = c.Amount,
                                  SalesMasterId = masterdata.Entity.Id,
                              };
                _context.SalesDetails.AddRange(details);
                _context.SaveChanges();

                foreach (var item in model.Sales)
                {


                    var currentItemInfo = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (currentItemInfo != null)
                    {
                        currentItemInfo.quantity -= item.quantity;
                        _context.ItemsCurrentInfo.Update(currentItemInfo);
                        _context.SaveChanges();
                    }

                    var historyInfo = new ItemCurrentInfoHistory
                    {
                        Id = 0,
                        ItemId = item.ItemId,
                        Quantity = item.quantity,
                        TransDate = DateTime.Now,
                        StockCheckOut = StockCheckOut.Out,
                        TransactionType = TransactionType.Sales
                    };
                    _context.ItemsHistoryInfo.Add(historyInfo);
                    _context.SaveChanges();
                }
            }
            return new ResponseModel { Success = true, Message = "Sales added successfully." };
        }
        public int Delete(int id)
        {
            var existingmasterdata = _context.SalesMaster.Find(id);
            if (existingmasterdata == null)
            {
                return 0;
            }
            else
            {
                var existingdetailsdata = _context.SalesDetails.Where(x => x.SalesMasterId == existingmasterdata.Id).ToList();
                if (existingdetailsdata.Count > 0)
                {
                    foreach (var item in existingdetailsdata)
                    {

                        var CurrentItemsInfo = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == item.ItemId);
                        if (CurrentItemsInfo != null)
                        {
                            CurrentItemsInfo.quantity -= item.quantity;
                            _context.ItemsCurrentInfo.Update(CurrentItemsInfo);
                            _context.SaveChanges();
                        }
                        var historyInfoData = new ItemCurrentInfoHistory
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            Quantity = item.quantity,
                            TransDate = DateTime.Now,
                            StockCheckOut = StockCheckOut.Out,
                            TransactionType = TransactionType.Sales
                        };
                        _context.ItemsHistoryInfo.Add(historyInfoData);
                        _context.SaveChanges();

                    }
                    _context.SalesDetails.RemoveRange(existingdetailsdata);
                    _context.SaveChanges();
                };

                _context.SalesMaster.Remove(existingmasterdata);
                _context.SaveChanges();
                return 1;
            }
        }


        public ResponseModel Updates(SalesMasterVM obj)
        {
            if (obj.Sales.Count == 0)
            {
                return new ResponseModel { Success = false, Message = "No items to update." };
            }

            var existingmasterdata = _context.SalesMaster.Find(obj.Id);
            if (existingmasterdata == null)
            {
                return new ResponseModel { Success = false, Message = "No items to update." };
            }
            else
            {
                // Check item availability
                foreach (var item in obj.Sales)
                {
                    var existingItem = _context.SalesDetails
                        .FirstOrDefault(x => x.SalesMasterId == existingmasterdata.Id && x.ItemId == item.ItemId);

                    int quantityDifference = existingItem != null ? item.quantity - existingItem.quantity : item.quantity;

                    //int quantityDifference;
                    //if (existingItem != null)
                    //{
                    //    quantityDifference = item.quantity - existingItem.quantity;
                    //}
                    //else
                    //{
                    //    quantityDifference = item.quantity;
                    //}



                    if (quantityDifference > 0 && !IsItemAvailable(item.ItemId, quantityDifference))
                    {
                        string itemName = GetItemName(item.ItemId);
                        return new ResponseModel { Success = false, Message = $"Item {itemName} is not available in the required quantity." };
                    }
                }

                existingmasterdata.SalesDate = obj.SalesDate;
                existingmasterdata.CustomerId = obj.CustomerId;
                existingmasterdata.InvoiceNumber = obj.InvoiceNumber;
                existingmasterdata.BillAmount = obj.BillAmount;
                existingmasterdata.Discount = obj.Discount;
                existingmasterdata.NetAmount = obj.NetAmount;

                var masterAdd = _context.SalesMaster.Update(existingmasterdata);
                _context.SaveChanges();


                var existingDetailsData = _context.SalesDetails.Where(x => x.SalesMasterId == existingmasterdata.Id).ToList();

                foreach (var item in existingDetailsData)
                {
                    var itemcurrentinfo = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (itemcurrentinfo != null)
                    {
                        itemcurrentinfo.quantity += item.quantity;
                        _context.ItemsCurrentInfo.Update(itemcurrentinfo);
                        _context.SaveChanges();
                    }

                    var historyinfo = new ItemCurrentInfoHistory
                    {
                        Id = 0,
                        ItemId = item.ItemId,
                        Quantity = item.quantity,
                        TransDate = DateTime.Now,
                        StockCheckOut = StockCheckOut.In,
                        TransactionType = TransactionType.Sales
                    };
                    _context.ItemsHistoryInfo.Add(historyinfo);
                    _context.SaveChanges();
                };

                _context.SalesDetails.RemoveRange(existingDetailsData);


                var details = from c in obj.Sales
                              select new SalesDetailsModel
                              {
                                  Id = 0,
                                  ItemId = c.ItemId,
                                  Unit = c.Unit,
                                  quantity = c.quantity,
                                  price = c.price,
                                  Amount = c.Amount,
                                  SalesMasterId = masterAdd.Entity.Id
                              };
                _context.SalesDetails.AddRange(details);
                _context.SaveChanges();
                foreach (var item in obj.Sales)
                {



                    var currentItemInfo = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (currentItemInfo != null)
                    {

                        currentItemInfo.quantity -= item.quantity;
                        _context.ItemsCurrentInfo.Update(currentItemInfo);
                        _context.SaveChanges();


                        var historyInfoData = new ItemCurrentInfoHistory
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            Quantity = item.quantity,
                            TransDate = DateTime.Now,
                            StockCheckOut = StockCheckOut.In,
                            TransactionType = TransactionType.Sales
                        };
                        _context.ItemsHistoryInfo.Add(historyInfoData);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var newitemsInfo = new ItemCurrentInfo
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            quantity = item.quantity
                        };
                        _context.ItemsCurrentInfo.Add(newitemsInfo);
                        _context.Add(newitemsInfo);
                        _context.SaveChanges();

                        var historyInfoData = new ItemCurrentInfoHistory
                        {
                            Id = 0,
                            ItemId = item.ItemId,
                            Quantity = item.quantity,
                            TransDate = DateTime.Now,
                            StockCheckOut = StockCheckOut.In,
                            TransactionType = TransactionType.Sales
                        };
                        _context.ItemsHistoryInfo.Add(historyInfoData);
                        _context.SaveChanges();
                    }
                }
                return new ResponseModel { Success = true, Message = "Sales updated successfully." };
            };
        }

        public IEnumerable<GetCustomersNameVM> GetCustomersName()
        {
            var customers = _context.Customers.Select(customer => new GetCustomersNameVM
            {
                CustomerId = customer.Id,
                CustomerName = customer.FullName
            }).ToList();
            return customers.AsEnumerable();
        }

        public IEnumerable<GetItemsNameVM> GetItemsName()
        {
            var items = (from item in _context.Items
            join Itemsquantity in _context.ItemsCurrentInfo
            on item.Id equals Itemsquantity.ItemId
            select new GetItemsNameVM
            {
                ItemId = item.Id,
                ItemName = item.Name,
                ItemUnit = item.Unit,
                quantity = Itemsquantity.quantity
            }).ToList();
            return items;
        }
    }
}
