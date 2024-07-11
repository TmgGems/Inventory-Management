using Inventory_Management.Models;
using Inventory_Management.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Inventory_Management.Utils;

namespace Inventory_Management.Controllers
{
    [AllowAnonymous]
    public class LogInController : Controller
    {
        IUserService _userService;
        public LogInController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LogInModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = _userService.GetUserWithRole(model.Username, model.PassWord);
                if (user != null)
                {
                    IdentityUtils.AddingClaimIdentity(model,user.Roles ?? "user", HttpContext);
                    return Redirect("/");
                }
                else
                    ModelState.AddModelError("Password", "Invalid Username or Password");
            }

            return View("Index");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "LogIn");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpModel modeldata)
        {
            if (ModelState.IsValid)
            {
                UserModel model = new UserModel()
                {
                    Username = modeldata.Email,
                    Password = modeldata.Password,
                    Roles = "user"
                };
               bool result =  _userService.RegisterUser(model);
                if(result)
                {
                    return RedirectToAction("Index","LogIn");
                }
                return View();
            }
            return View();
        }
    }
}
