using Inventory.Services.Item.ViewModel;
using Inventory.Services.PurchaseMasterDetail;
using Inventory.Services.PurchaseMasterDetail.ViewModel;
using Inventory.Services.Vendor.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PurchaseMasterDetailsAPIController : ControllerBase
    {
        private IPurchaseMD _purchaseService;
        public PurchaseMasterDetailsAPIController(IPurchaseMD purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]

        public List<PurchaseMasterVM> GetAll()
        {
            return _purchaseService.GetAll();
        }

        [HttpGet]
        public PurchaseMasterVM GetById(int id)
        {
            return _purchaseService.GetById(id);
        }

        [HttpPost]
         public bool Add(PurchaseMasterVM model)
        {
            return _purchaseService.Add(model);
        }

        [HttpDelete]
        public int Delete(int id)
        {
            return _purchaseService.Delete(id);
        }

        [HttpPut]
        public bool Updates(PurchaseMasterVM obj)
        {
            return _purchaseService.Updates(obj);
        }

        [HttpGet]
        public IEnumerable<GetItemsNameVM> GetItemsName()
        {
            return _purchaseService.GetItemsName();
        }

        [HttpGet]
        public IEnumerable<GetVendorsName> GetVendorsName()
        {
            return _purchaseService.GetVendorsName();
        }
    }
}
