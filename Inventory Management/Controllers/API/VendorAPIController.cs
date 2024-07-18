using Inventory.Entities;
using Inventory.Services.Vendor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Pkcs;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class VendorAPIController : ControllerBase
    {
        private readonly IVendor _vendorservice;
        public VendorAPIController(IVendor vendor)
        {
            _vendorservice = vendor;
        }
        [HttpGet]

        public List<VendorModel> GetVendorList()
        {
            return _vendorservice.GetVendorList();
        }

        [HttpGet]
        public VendorModel GetVendorById(int vendorId)
        {
            return _vendorservice.GetVendorById(vendorId);
        }

        [HttpPost]
        public bool AddVendor(VendorModel vendor)
        {
            return _vendorservice.AddVendor(vendor);
        }

        [HttpPut]
        public bool UpdateVendor(VendorModel vendor)
        {
            return _vendorservice.UpdateVendor(vendor);
        }
        [HttpDelete]

        public bool DeleteModel(int vendorId)
        {
            return _vendorservice.DeleteModel(vendorId);
        }
    }
}
