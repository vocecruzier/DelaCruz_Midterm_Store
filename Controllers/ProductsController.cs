using Microsoft.AspNetCore.Mvc;
using DelaCruz_Midterm_Store.Data;
using DelaCruz_Midterm_Store.Models;

namespace DelaCruz_Midterm_Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductsController(ApplicationDbContext db) { _db = db; }

        // ✿ READ + SEARCH ✿ (◕‿◕)
        public IActionResult Index(string? searchString)
        {
            var products = _db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
{
            var search = searchString.ToLower();   // lowercase the search word
             products = products.Where(p => p.Name.ToLower().Contains(search)
                                || p.Category.ToLower().Contains(search)
                                || p.Description.ToLower().Contains(search));
}
            ViewData["SearchString"] = searchString;
            return View(products.ToList());
        }

        // ✿ CREATE ✿ ♡
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✿ UPDATE ✿ ٩(◕‿◕)۶
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✿ DELETE ✿ (╥﹏╥)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                // also clear it from the cart so no orphaned items remain
                _db.CartItems.RemoveRange(_db.CartItems.Where(c => c.ProductId == id));
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}