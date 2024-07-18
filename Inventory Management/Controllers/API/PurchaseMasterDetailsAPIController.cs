using Inventory.Services.PurchaseMasterDetail;
using Inventory.Services.PurchaseMasterDetail.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseMasterDetailsAPIController : ControllerBase
    {
        private IPurchaseMD _purchaseService;
        public PurchaseMasterDetailsAPIController(IPurchaseMD purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
         public bool Add(PurchaseMasterVM model)
        {
            return _purchaseService.Add(model);
        }
    }
}
