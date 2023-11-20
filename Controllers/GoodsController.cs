using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisasterAlleviation.Models;
using DisasterAlleviation.Goods;
using DisasterAlleviation.Category;
using DisasterAlleviation.Disaster;

namespace DisasterAlleviation.Controllers
{
    public class GoodsController : Controller
    {
        List<DonGoodsModel> GoodModel = new List<DonGoodsModel>();
        GoodsFunctions Goods = new GoodsFunctions();
        // GET: DonGoodsController
        public ActionResult Index()
        {
            Goods.GetDonations(out GoodModel);
            return View(GoodModel);
        }

        public ActionResult Details(int id)
        {
            var disasters = DisasterFunctions.GetDisaster(id);
            return View(disasters);
        }

        // GET: DonGoodsController/Create
        public ActionResult Create()
        {
            List<GoodsCatModel> Categories = new List<GoodsCatModel>();
            CategoryFunctions.GetCatList(out Categories);
            var items = Categories;
            if (items != null)
            {
                ViewBag.data = items;
            }
            List<DisasterModel> Disaster = new List<DisasterModel>();
            DisasterFunctions.GetDisasters(out Disaster);
            var items2 = Disaster;
            if (items2 != null)
            {
                ViewBag.data2 = items2;
            }
            return View();
        }

        // POST: DonGoodsController/Create
        public IActionResult CreateProccess(
        [Bind("ID,Date,DisasterID,DisasterDescription,Noitems,Category,Description,Anonymous,Username")] DonGoodsModel don)
        {
            Goods.AddDonation(don);
            Goods.GetDonations(out GoodModel);
            return View("Index", GoodModel);
        }

}
}
