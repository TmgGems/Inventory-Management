using Inventory.Entities;
using Inventory.Services.Item.ViewModel;
using Inventory.Services.PurchaseMasterDetail.ViewModel;
using Inventory.Services.Vendor.ViewModel;
using Inventory_Management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.PurchaseMasterDetail
{
    public class PurchaseMD : IPurchaseMD
    {
        private ApplicationDbContext _context;
        public PurchaseMD(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(PurchaseMasterVM model)
        {

            if (model != null)
            {
                if (model.PurchaseDetails == null)
                {
                    return false;
                }
                var masterpurchase = new PurchaseMasterModel()
                {
                    Id = 0,
                    VendorId = model.VendorId,
                    InvoiceNumber = model.InvoiceNumber,
                    BillAmount = model.BillAmount,
                    Discount = model.Discount,
                    NetAmount = model.NetAmount
                };
                var masterdata = _context.PurchaseMaster.Add(masterpurchase);
                _context.SaveChanges();
                if (masterdata != null)
                {
                    var details = from c in model.PurchaseDetails
                                  select new PurchaseDetailModel
                                  {
                                      Id = 0,
                                      ItemId = c.ItemId,
                                      Unit = c.Unit,
                                      Quantity = c.Quantity,
                                      Amount = c.Amount,
                                      PurchaseMasterId = masterdata.Entity.Id
                                  };
                    _context.PurchaseDetail.AddRange(details);
                    _context.SaveChanges();

                    foreach (var itemdetail in model.PurchaseDetails)
                    {
                        var currentItemInfo = _context.ItemsCurrentInfo.FirstOrDefault(i => i.ItemId == itemdetail.ItemId);
                        if (currentItemInfo != null)
                        {
                            currentItemInfo.quantity += itemdetail.Quantity;
                            _context.ItemsCurrentInfo.Update(currentItemInfo);
                            _context.SaveChanges();
                        }
                        else
                        {
                            var updateinfo = new ItemCurrentInfo
                            {
                                ItemId = itemdetail.ItemId,
                                quantity = itemdetail.Quantity
                            };
                            _context.ItemsCurrentInfo.Add(updateinfo);
                            _context.SaveChanges();

                        }
                        var historyEntry = new ItemCurrentInfoHistory
                        {
                            Id = 0,
                            ItemId = itemdetail.ItemId,
                            Quantity = itemdetail.Quantity,
                            TransDate = DateTime.Now,
                            StockCheckOut = StockCheckOut.In,
                            TransactionType = TransactionType.Sales
                        };
                        _context.ItemsHistoryInfo.Add(historyEntry);
                        _context.SaveChanges();
                    }

                }
                return true;
            }
            return false;
        }

        public int Delete(int id)
        {
            var existingMasterData = _context.PurchaseMaster.Find(id);
            if (existingMasterData == null)
            {
                return 0;
            }
            else
            {
                var existingdetailsdata = _context.PurchaseDetail.Where(x => x.PurchaseMasterId == existingMasterData.Id).ToList();

                foreach (var item in existingdetailsdata)
                {
                    var existingItemStatus = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == item.ItemId);
                    if (existingItemStatus != null)
                    {
                        existingItemStatus.quantity -= item.Quantity;
                        _context.ItemsCurrentInfo.Update(existingItemStatus);
                        _context.SaveChanges();
                    }

                    var itemsinfohistory = new ItemCurrentInfoHistory
                    {
                        Id = 0,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity,
                        TransDate = DateTime.Now,
                        StockCheckOut = StockCheckOut.Out,
                        TransactionType = TransactionType.Sales
                    };
                    _context.ItemsHistoryInfo.Add(itemsinfohistory);
                    _context.SaveChanges();

                }
                _context.PurchaseDetail.RemoveRange(existingdetailsdata);
                _context.SaveChanges();
                _context.PurchaseMaster.Remove(existingMasterData);
                _context.SaveChanges();
                return id;
            }
        }
        public bool Updates(PurchaseMasterVM obj)
        {
            var existingMasterData = _context.PurchaseMaster.Find(obj.Id);
            if (existingMasterData == null)
            {
                return false;
            }
            existingMasterData.Id = obj.Id;
            existingMasterData.VendorId = obj.VendorId;
            existingMasterData.InvoiceNumber = obj.InvoiceNumber;
            existingMasterData.BillAmount = obj.BillAmount;
            existingMasterData.Discount = obj.Discount;
            existingMasterData.NetAmount = obj.NetAmount;
            var masterdata = _context.PurchaseMaster.Update(existingMasterData);
            _context.SaveChanges();

            var existingDetailsData = _context.PurchaseDetail.Where(x => x.PurchaseMasterId == existingMasterData.Id).ToList();

            foreach (var item in existingDetailsData)
            {
                var itemcurrentinfo = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == item.ItemId);
                if (itemcurrentinfo != null)
                {
                    itemcurrentinfo.quantity -= item.Quantity;
                    _context.ItemsCurrentInfo.Update(itemcurrentinfo);
                    _context.SaveChanges();
                }

                var historyinfo = new ItemCurrentInfoHistory
                {
                    Id = 0,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    TransDate = DateTime.Now,
                    StockCheckOut = StockCheckOut.Out,
                    TransactionType = TransactionType.Sales
                };
                _context.ItemsHistoryInfo.Add(historyinfo);
                _context.SaveChanges();
            };
            _context.PurchaseDetail.RemoveRange(existingDetailsData);

            var detailsdata = from c in obj.PurchaseDetails
                              select new PurchaseDetailModel
                              {
                                  Id = 0,
                                  ItemId = c.ItemId,
                                  Unit = c.Unit,
                                  Quantity = c.Quantity,
                                  Amount = c.Amount,
                                  PurchaseMasterId = masterdata.Entity.Id
                              };
            _context.PurchaseDetail.AddRange(detailsdata);
            _context.SaveChanges();
            foreach (var item in detailsdata)
            {
                var itemcurrentinfo = _context.ItemsCurrentInfo.FirstOrDefault(x => x.ItemId == item.ItemId);
                if (itemcurrentinfo != null)
                {
                    itemcurrentinfo.quantity += item.Quantity;
                    _context.ItemsCurrentInfo.Update(itemcurrentinfo);
                    _context.SaveChanges();
                }

                var historyinfo = new ItemCurrentInfoHistory
                {
                    Id = 0,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    TransDate = DateTime.Now,
                    StockCheckOut = StockCheckOut.In,
                    TransactionType = TransactionType.Purchase
                };
                _context.ItemsHistoryInfo.Add(historyinfo);
                _context.SaveChanges();
            };


            return true;
        }

        public List<PurchaseMasterVM> GetAll()
        {
            List<PurchaseMasterVM> dataList = new List<PurchaseMasterVM>();
            var existingMasterdatas = _context.PurchaseMaster.ToList();
            if (existingMasterdatas != null)
            {
                foreach (var masterdata in existingMasterdatas)
                {
                    var detailsdata = _context.PurchaseDetail.Where(x => x.PurchaseMasterId == masterdata.Id).Include(x => x.Item).ToList();
                    var vendorsdata = _context.Vendors.FirstOrDefault(x => x.Id == masterdata.VendorId);
                    var masterdatas = new PurchaseMasterVM
                    {
                        Id = masterdata.Id,
                        VendorId = masterdata.VendorId,
                        VendorName = vendorsdata.Name,
                        InvoiceNumber = masterdata.InvoiceNumber,
                        BillAmount = masterdata.BillAmount,
                        Discount = masterdata.Discount,
                        NetAmount = masterdata.NetAmount,
                    };

                    masterdatas.PurchaseDetails = (from d in detailsdata

                                                   select new PurchaseDetailVM
                                                   {
                                                       Id = d.Id,
                                                       ItemId = d.ItemId,
                                                       ItemName = d.Item.Name,
                                                       Unit = d.Unit,
                                                       Quantity = d.Quantity,
                                                       Amount = d.Amount
                                                   }).ToList();
                    dataList.Add(masterdatas);
                }
            }
            return dataList;
        }

        public PurchaseMasterVM GetById(int id)
        {
            var masterdata = _context.PurchaseMaster.Find(id);
            if (masterdata == null)
            {
                return new PurchaseMasterVM();
            }
            else
            {
                var detaildatas = _context.PurchaseDetail.Where(x => x.PurchaseMasterId == masterdata.Id).Include(x => x.Item).ToList();
                var vendordatas = _context.Vendors.FirstOrDefault(x => x.Id == masterdata.VendorId);
                var masterdatas = new PurchaseMasterVM
                {
                    Id = masterdata.Id,
                    VendorId = masterdata.VendorId,
                    VendorName = vendordatas.Name,
                    InvoiceNumber = masterdata.InvoiceNumber,
                    BillAmount = masterdata.BillAmount,
                    Discount = masterdata.Discount,
                    NetAmount = masterdata.NetAmount
                };

                masterdatas.PurchaseDetails = (from detail in detaildatas
                                               select new PurchaseDetailVM
                                               {
                                                   Id = detail.Id,
                                                   ItemId = detail.Item.Id,
                                                   ItemName = detail.Item.Name,
                                                   Unit = detail.Unit,
                                                   Quantity = detail.Quantity,
                                                   Amount = detail.Amount
                                               }).ToList();
                return masterdatas;
            }
        }

        public IEnumerable<GetItemsNameVM> GetItemsName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetVendorsName> GetVendorsName()
        {
            throw new NotImplementedException();
        }


    }
}
