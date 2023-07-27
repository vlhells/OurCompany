using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Practice_1.Admin.Models;
using Practice_1.Areas.Admin.Models;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Practice_1.Controllers
{
    public class HomeController: Controller
    {
        ApplicationContext db;

        public HomeController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            var form = Request.Form;
            if (!form.ContainsKey("email") || !form.ContainsKey("password"))
                return Content("Email и/или пароль не введены");

            string email = form["email"];
            string password = form["password"];

            User? user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user is null) return Content("Не авторизован");

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email), new Claim("Role", user.Role) };
            ClaimsIdentity claimsIdentity= new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task AccessDenied()
        {
			HttpContext.Response.StatusCode = 403;
			await HttpContext.Response.WriteAsync("Access Denied");
		}
    }
}
