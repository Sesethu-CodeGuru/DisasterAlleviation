using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.PurchaseGoods;
using DisasterAlleviation.Inventory;

namespace DisasterAlleviation.Controllers
{
    public class PurchasedGoodsController : Controller
    {
        List<GoodsPurchaseCatModel> purchase = new List<GoodsPurchaseCatModel>();
        public IActionResult Index()
        {
            PurchaseGoodsFunctions.GetPurchase(out purchase);
            return View(purchase);
        }
        public IActionResult Donate(int id)
        {
            PurchaseGoodsFunctions.Withdraw(id);
            InventoryFunctions.AddToInventory(id);
            PurchaseGoodsFunctions.GetPurchase(out purchase);
            return View("Index", purchase);
        }
    }
}
