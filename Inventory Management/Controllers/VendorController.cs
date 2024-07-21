using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    public class VendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
