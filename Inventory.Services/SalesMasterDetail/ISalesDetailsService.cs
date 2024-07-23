﻿
using Inventory.Services.Item.ViewModel;
using Inventory.Services.MasterDetail.ViewModel;
using Inventory.Services.Modell;

namespace Inventory.Services.MasterDetail
{
    public interface ISalesDetailsService
    {
        List<SalesMasterVM> GetAll();

        SalesMasterVM GetById(int id);

        ResponseModel Add(SalesMasterVM model);

        ResponseModel Updates(SalesMasterVM obj);

        int Delete(int id);

        public IEnumerable<GetCustomersNameVM> GetCustomersName();

        IEnumerable<GetItemsNameVM> GetItemsName();
    }
}
