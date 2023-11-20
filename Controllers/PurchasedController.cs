using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;

namespace DisasterAlleviation.Controllers
{
    public class PurchasedController : Controller
    {
        List<DonPurchasedModel> PGoods = new List<DonPurchasedModel>();
        List<PurchasedInventoryModel> inventories = new List<PurchasedInventoryModel>();
        PurchasedInventoryModel inventory = new PurchasedInventoryModel();
        public IActionResult Index()
        {
            Purchased.PurchasedFunctions.GetGoods(out PGoods);
            return View(PGoods);
        }
        public IActionResult Create()
        {
            Inventory.InventoryFunctions.GetInventory(out inventories);
            return View(inventories);
        }
        public IActionResult Donate(int id)
        {
            Inventory.InventoryFunctions.GetInventory(id, out inventory);
            ViewBag.Category = inventory.Category;
            ViewBag.Items = inventory.Noitems;
            List<DisasterModel> Disasters = new List<DisasterModel>();
            Disaster.DisasterFunctions.GetDisasters(out Disasters);
            var items = Disasters;
            if (items != null)
            {
                ViewBag.data = items;
            }
            return View();
        }
        public IActionResult CreateProccess(
            [Bind("id, Date,DisasterID,DisasterDescription, Noitems, Category, Anonymous")]
            DonPurchasedModel good
        )
        {
            Purchased.PurchasedFunctions.AddGoods(good);
            Purchased.PurchasedFunctions.GetGoods(out PGoods);
            return View("Index", PGoods);
        }
        public ActionResult Details(int id)
        {
            var disasters = Disaster.DisasterFunctions.GetDisaster(id);
            return View(disasters);
        }
    }
}
