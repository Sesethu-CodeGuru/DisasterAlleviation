using DisasterAlleviation.Models;
using Microsoft.AspNetCore.Mvc;
using DisasterAlleviation.Category;
namespace DisasterAlleviation.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        // POST: DonGoodsController/Create
        public IActionResult CreateProccess(
        [Bind("Category")] string Category)
        {
            CategoryFunctions.AddCategory(Category);
            return RedirectToAction("Create", "DonGoods", new { area = "" });
        }
    }
}
