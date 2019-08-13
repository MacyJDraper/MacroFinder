using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MacroFinder1.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MacroFinder1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult AddProduct()
        {
            ViewData["Message"] = "Add Your Product";

            return View();
        }
        public IActionResult Add(Product prod)
        {
            ProductRepository repo = new ProductRepository();
            repo.AddProductToDatabase(prod);
            return RedirectToAction("Index", "Product");
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            ProductRepository repo = new ProductRepository();
            List<Models.Product> products = repo.GetAllProducts();
            return View(products);
        }
    }
}
