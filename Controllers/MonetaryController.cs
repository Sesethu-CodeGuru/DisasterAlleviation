using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Models;
using DisasterAlleviation.Monetary;

namespace DisasterAlleviation.Controllers
{
    public class MonetaryController : Controller
    {
        List<DonMonetaryModel> MonterayModel = new List<DonMonetaryModel>();
        MonetaryFunctions Monetary = new MonetaryFunctions();
        // GET: DonMonetaryController
        public ActionResult Index()
        {
            Monetary.GetDonations(out MonterayModel);
            return View(MonterayModel);
        }

        // GET: DonMonetaryController/Details/5
        public ActionResult Details(int id)
        {
            var Donation = Monetary.GetDonation(id);
            return View(Donation);
        }

        // GET: DonMonetaryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonMonetaryController/Create
        public IActionResult CreateProccess(
        [Bind("ID,Date,Amount,Username,Anonymous")] DonMonetaryModel don)
        {
            Monetary.AddDonation(don);
            Monetary.GetDonations(out MonterayModel);
            return View("Index", MonterayModel);
        }
    }
}
