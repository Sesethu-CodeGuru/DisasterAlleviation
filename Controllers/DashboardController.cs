﻿using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Monetary;
using DisasterAlleviation.Purchased;
using DisasterAlleviation.Disaster;
using DisasterAlleviation.Models;
using DisasterAlleviation.Goods;

namespace DisasterAlleviation.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel();

            //total monetary donations
            List<DonMonetaryModel> monetaryDonations;
            MonetaryFunctions monetaryFunctions = new MonetaryFunctions();
            monetaryFunctions.GetDonations(out monetaryDonations);
            ViewBag.TotalMonetaryDonations = monetaryDonations.Sum(d => d.Amount);

            //total number of goods received
            List<DonGoodsModel> allGoodsDonations;
            GoodsFunctions.GetAllGoodsDonations(out allGoodsDonations);
            ViewBag.TotalGoodsReceived = allGoodsDonations.Sum(g => g.Noitems);

            //active disasters with allocated money and goods
            List<DisasterModel> activeDisasters;
            DisasterFunctions.GetDisasters(out activeDisasters);
            ViewBag.ActiveDisasters = activeDisasters;

            return View(viewModel);
        }
    }
}