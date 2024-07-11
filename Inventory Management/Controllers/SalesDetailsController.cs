using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    [Authorize(Roles = "admin")]
    public class SalesDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
