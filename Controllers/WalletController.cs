using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Models;

namespace DisasterAlleviation.Controllers
{
    public class WalletController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Fund(
           [Bind("id,Username,Amount")]
             WalletModel wallet
       )
        {
            Wallet.WalletFunctions.AddFunds(wallet);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
