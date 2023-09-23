using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication6.Migrations;
using WebApplication6.Models;
using Microsoft.AspNetCore.Http;
using UserT = WebApplication6.Models.UserT;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDBContext context;
        public HomeController(UserDBContext context)
        {
            this.context = context; 
        }
      

        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserT user)
        {
            var myuser = context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if (myuser != null)
            {
                HttpContext.Session.SetString("UserSession", myuser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed";
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserT user)
        {
            if (ModelState.IsValid)
            { 
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                TempData["succes"] = "Register succesfully";
                return RedirectToAction("Login");
            }
            return View();
        }


        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession");
            }
            else 
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}