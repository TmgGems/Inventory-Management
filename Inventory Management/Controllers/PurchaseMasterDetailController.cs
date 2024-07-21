using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    public class PurchaseMasterDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
