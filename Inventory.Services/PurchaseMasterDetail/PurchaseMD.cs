using Inventory.Entities;
using Inventory.Services.Item.ViewModel;
using Inventory.Services.PurchaseMasterDetail.ViewModel;
using Inventory.Services.Vendor.ViewModel;
using Inventory_Management.Data;
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
                        }
                        else
                        {
                            var updateinfo = new ItemCurrentInfo
                            {
                                ItemId = itemdetail.ItemId,
                                quantity = itemdetail.Quantity
                            };
                            _context.ItemsCurrentInfo.Update(updateinfo);
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
            throw new NotImplementedException();
        }

        public List<PurchaseMasterVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public PurchaseMasterVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetItemsNameVM> GetItemsName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetVendorsName> GetVendorsName()
        {
            throw new NotImplementedException();
        }

        public bool Updates(PurchaseMasterVM obj)
        {
            throw new NotImplementedException();
        }
    }
}
