using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Users;

namespace DisasterAlleviation.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            UserFunctions.LogOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
