using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Users;

namespace DisasterAlleviation.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProcessLogin(UserModel user)
        {
            if (UserFunctions.FindUserByUsernameAndPassword(user.Username, user.Password))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                ViewBag.Status = "Unsuccessful Login Attempt";
                return View("Index");
            }
        }
    }
}
