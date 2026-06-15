using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class ProductController : Controller
    {
        ProductBL productBL = new ProductBL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            List<Product> products = productBL.GetAll();
            return View("ShowAll", products);
        }

        public IActionResult Details(int id)
        {
            Product product = productBL.GetById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }
    }
}
