using Inventory_Management.Models;
using Inventory_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerAPIController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]

        public List<CustomerModel> GetAll()
        {
            return _customerService.GetAll();
        }

        [HttpGet("{id}")]

        public CustomerModel GetById(int id)
        {
            return _customerService.GetById(id);
        }

        [HttpPost]
        public bool Add(CustomerModel obj)
        {
            return _customerService.Add(obj);
        }

        [HttpPut]
        public bool Updates(CustomerModel obj)
        {
            return _customerService.Updates(obj);
        }

        [HttpDelete]
        public int Delete(int id)
        {
            return _customerService.Delete(id);
        }
    }
}
