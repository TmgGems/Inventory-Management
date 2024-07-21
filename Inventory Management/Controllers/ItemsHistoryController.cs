using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    public class ItemsHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
