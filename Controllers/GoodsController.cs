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

        // GET: DonGoodsController/Details/5
        public ActionResult Details(int id)
        {
            var Donation = Goods.GetDonation(id);
            return View(Donation);
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
            return View();
        }

        // POST: DonGoodsController/Create
        public IActionResult CreateProccess(
        [Bind("ID,Date,Noitems,Category,Description,Anonymous,Username")] DonGoodsModel don)
        {
            Goods.AddDonation(don);
            Goods.GetDonations(out GoodModel);
            return View("Index", GoodModel);
        }

}
}
