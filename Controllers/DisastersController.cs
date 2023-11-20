using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Disaster;

namespace DisasterAlleviation.Controllers
{
    public class DisastersController : Controller
    {
        List<DisasterModel> DisasterModel = new List<DisasterModel>();
        public IActionResult Index()
        {
            DisasterFunctions.GetDisasters(out DisasterModel);
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
            DisasterFunctions.AddDisaster(disasters);
            DisasterFunctions.GetDisasters(out DisasterModel);
            return View("Index", DisasterModel);
        }

        public ActionResult Details(int id)
        {
            var disasters = DisasterFunctions.GetDisaster(id);
            return View(disasters);
        }
    }
}
