using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
