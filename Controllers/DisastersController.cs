using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Disaster;

namespace DisasterAlleviation.Controllers
{
    public class DisastersController : Controller
    {
        List<DisasterModel> DisasterModel = new List<DisasterModel>();
        DisasterFunctions disaster = new DisasterFunctions();
        public IActionResult Index()
        {
            disaster.GetDisasters(out DisasterModel);
            return View(DisasterModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        // POST: DonGoodsController/Create
        public IActionResult CreateProccess(
        [Bind("ID,Startdate,Enddate,Location,Description,Aidtype")] DisasterModel disasters)
        {
            disaster.AddDisaster(disasters);
            disaster.GetDisasters(out DisasterModel);
            return View("Index", DisasterModel);
        }

        public ActionResult Details(int id)
        {
            var disasters = disaster.GetDisaster(id);
            return View(disasters);
        }
    }
}
