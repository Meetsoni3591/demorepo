using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApplication6.Models;
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
      
        /// <summary>
        /// Normal Index view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction("Login"); // default view on start 
        }

        /// <summary>
        /// Login Actoin with session
        /// </summary>
        /// <returns></returns>
        public  IActionResult Login()
        {
            try 
            {
                if(HttpContext.Session.GetString("UserSession") != null)
                return RedirectToAction("Dashboard");
            }
            catch
            {
                Console.WriteLine("error occured");
            }

            return View();
        }

        /// <summary>
        /// Check that user is register or not 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(UserT user)
        {
            try
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
            }
            catch(Exception ex){
                Console.WriteLine(ex.ToString());   
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// If register Successfilly redirect to Login for dashboard
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(UserT user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    TempData["succes"] = "Register succesfully";
                    return RedirectToAction("Login");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View();
        }

        /// <summary>
        /// Dashboard view for Registered users
        /// </summary>
        /// <returns></returns>
        public IActionResult Dashboard()
        {
            try
            {
                if (HttpContext.Session.GetString("UserSession") != null)
                {
                    ViewBag.MySession = HttpContext.Session.GetString("UserSession");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Action for user logout removing  session
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            try
            {
                if (HttpContext.Session.GetString("UserSession") != null)
                {
                    HttpContext.Session.Remove("UserSession");
                    return RedirectToAction("Login");
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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