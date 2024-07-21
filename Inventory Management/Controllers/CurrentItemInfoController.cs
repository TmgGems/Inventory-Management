using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    public class CurrentItemInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
