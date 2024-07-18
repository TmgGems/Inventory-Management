using Inventory.Entities;
using Inventory_Management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Vendor
{
    public class Vendor : IVendor
    {
        private readonly ApplicationDbContext _context;
        public Vendor(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddVendor(VendorModel vendor)
        {
            if(vendor == null)
            {
                return false;
            }
            _context.Vendors.Add(vendor);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteModel(int vendorId)
        {
            var exitingdata = _context.Vendors.Find(vendorId);
            if(exitingdata != null)
            {
                _context.Vendors.Remove(exitingdata);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public VendorModel GetVendorById(int vendorId)
        {
            if(vendorId == null)
            {
                return null;
            }
            var existingdata = _context.Vendors.Find(vendorId);
            return existingdata;
        }

        public List<VendorModel> GetVendorList()
        {
            var existingdataList = _context.Vendors.ToList();
            return existingdataList;
        }

        public bool UpdateVendor(VendorModel vendor)
        {
            var existingdata = _context.Vendors.Find(vendor.Id);
            if(existingdata != null)
            {
                existingdata.Name = vendor.Name;
                existingdata.Address = vendor.Address;
                existingdata.Contact = vendor.Contact;
                _context.Vendors.Update(existingdata);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
