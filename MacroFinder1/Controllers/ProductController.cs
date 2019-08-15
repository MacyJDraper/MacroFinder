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
        public IActionResult ViewProduct(int id)
        {
            ProductRepository repo = new ProductRepository();
            Models.Product productToView = repo.GetProduct(id);
            return View(productToView);
        }

        public IActionResult UpdateProduct(int id)
        {
            ProductRepository repo = new ProductRepository();
            Models.Product productToUpdate = repo.GetProduct(id);

            return View(productToUpdate);
        }
        public IActionResult UpdateProductToDataBase(Models.Product product)
        {
            ProductRepository repo = new ProductRepository();
            repo.UpdateProduct(product);
            return RedirectToAction("ViewProduct", new { id = product.Product_ID });

        }
        public IActionResult DeleteProduct(Product product)
        {
            ProductRepository repo = new ProductRepository();
            repo.DeleteProduct(product.Product_ID);

            return RedirectToAction("Index");
        }
    }
}
