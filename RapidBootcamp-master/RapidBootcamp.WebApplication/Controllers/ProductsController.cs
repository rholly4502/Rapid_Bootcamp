using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidBootcamp.WebApplication.DAL;

namespace RapidBootcamp.WebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;
        public ProductsController(IProduct product)
        {
            _product = product;
        }

        // GET: ProductsController1
        public ActionResult Index()
        {
            var models = _product.GetAll();
            return View(models);
        }

        // GET: ProductsController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
