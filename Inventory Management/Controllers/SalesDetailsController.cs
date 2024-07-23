using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    public class SalesDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
