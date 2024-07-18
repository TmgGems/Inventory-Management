using Inventory.Services.Item.ViewModel;
using Inventory.Services.MasterDetail.ViewModel;
using Inventory.Services.Modell;
using Inventory.Services.PurchaseMasterDetail.ViewModel;
using Inventory.Services.Vendor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.PurchaseMasterDetail
{
    public interface IPurchaseMD
    {
        List<PurchaseMasterVM> GetAll();

        PurchaseMasterVM GetById(int id);

        bool Add(PurchaseMasterVM model);

        bool Updates(PurchaseMasterVM obj);

        int Delete(int id);

        public IEnumerable<GetVendorsName> GetVendorsName();

        public IEnumerable<GetItemsNameVM> GetItemsName();
    }
}
