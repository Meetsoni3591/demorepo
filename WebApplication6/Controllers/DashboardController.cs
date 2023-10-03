using Microsoft.AspNetCore.Mvc;

namespace WebApplication6.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
