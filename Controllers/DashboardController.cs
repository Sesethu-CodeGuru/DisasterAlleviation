using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Monetary;
using DisasterAlleviation.Purchased;
using DisasterAlleviation.Disaster;
using DisasterAlleviation.Models;

namespace DisasterAlleviation.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel();

            // Get total monetary donations
            List<DonMonetaryModel> monetaryDonations;
            MonetaryFunctions monetaryFunctions = new MonetaryFunctions();
            monetaryFunctions.GetDonations(out monetaryDonations);
            ViewBag.TotalMonetaryDonations = monetaryDonations.Sum(d => d.Amount);

            // Get total number of goods received
            List<DonPurchasedModel> purchasedGoods;
            PurchasedFunctions.GetGoods(out purchasedGoods);
            ViewBag.TotalGoodsReceived = purchasedGoods.Sum(g => g.Noitems);

            // Get currently active disasters with allocated money and goods
            List<DisasterModel> activeDisasters;
            DisasterFunctions.GetDisasters(out activeDisasters);
            ViewBag.ActiveDisasters = activeDisasters;

            return View(viewModel);
        }
    }
}