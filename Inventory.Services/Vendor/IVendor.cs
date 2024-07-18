using Inventory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Vendor
{
    public interface IVendor
    {
        List<VendorModel> GetVendorList();

        VendorModel GetVendorById(int vendorId);

        bool AddVendor(VendorModel vendor); 

        bool DeleteModel(int vendorId);

        bool UpdateVendor(VendorModel vendor);
    }
}
