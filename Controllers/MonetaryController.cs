using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Models;
using DisasterAlleviation.Monetary;
using DisasterAlleviation.Disaster;

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
            var disasters = DisasterFunctions.GetDisaster(id);
            return View(disasters);
        }

        // GET: DonMonetaryController/Create
        public ActionResult Create()
        {
            List<DisasterModel> Disaster = new List<DisasterModel>();
            DisasterFunctions.GetDisasters(out Disaster);
            var items = Disaster;
            if (items != null)
            {
                ViewBag.data = items;
            }
            return View();
        }

        // POST: DonMonetaryController/Create
        public IActionResult CreateProccess(
        [Bind("ID,Date,DisasterID,DisasterDescription,Amount,Username,Anonymous")] DonMonetaryModel don)
        {
            Monetary.AddDonation(don);
            Monetary.GetDonations(out MonterayModel);
            return View("Index", MonterayModel);
        }
    }
}
