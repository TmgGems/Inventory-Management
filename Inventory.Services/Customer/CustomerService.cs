using Inventory_Management.Data;
using Inventory_Management.Models;

namespace Inventory.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(CustomerModel obj)
        {
            _context.Customers.Add(obj);
            _context.SaveChanges();
            return true;
        }

        public int Delete(int id)
        {
            var data = _context.Customers.Find(id);
            if (data != null)
            {
                _context.Customers.Remove(data);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public List<CustomerModel> GetAll()
        {
            var customers = _context.Customers.ToList();
            return customers;
        }

        public CustomerModel GetById(int id)
        {
            var customerData = _context.Customers.Find(id);
            if (customerData != null)
            {
                return customerData;
            }
            return null;
        }

        public bool Updates(CustomerModel obj)
        {
            var customerdata = _context.Customers.Find(obj.Id);
            if (customerdata != null)
            {
                customerdata.FullName = obj.FullName;
                customerdata.ContactNo = obj.ContactNo;
                customerdata.Address = obj.Address;
                _context.Customers.Update(customerdata);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
