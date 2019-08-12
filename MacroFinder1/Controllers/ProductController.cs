using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MacroFinder1.Controllers
{
    public class ProductController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Models.Product> products = new List<Models.Product>();
            products.Add(new Models.Product() { Name = "Dell Laptop", Price = 325 });
            products.Add(new Models.Product() { Name = "Lenovo Laptop", Price = 900 });
            products.Add(new Models.Product() { Name = "Mac Laptop", Price = 1200 });
            products.Add(new Models.Product() { Name = "Vacumm Cleaner", Price = 75 });
            products.Add(new Models.Product() { Name = "IRobot", Price = 20000000 });

            return View(products);
        }
    }
}
