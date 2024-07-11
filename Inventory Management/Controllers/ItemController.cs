using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controllers
{
    [Authorize(Roles ="user")]
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
