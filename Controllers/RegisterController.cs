using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Users;

namespace DisasterAlleviation.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProcessRegistry(UserModel user)
        {
            if (UserFunctions.FindUserByUsername(user.Username))
            {
                ViewBag.Status = "User Exists";
                return View();
            }
            else
            {
                UserFunctions.AddUserByUsernameAndPassword(user.Username, user.Password);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }
    }
}
