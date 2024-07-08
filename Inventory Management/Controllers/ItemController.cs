using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
