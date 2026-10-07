using Microsoft.AspNetCore.Mvc;
using DelaCruz_Midterm_Store.Data;
using DelaCruz_Midterm_Store.Models;

namespace DelaCruz_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db) { _db = db; }

        // ✿ READ cart ✿ 🛒
        public IActionResult Index()
        {
            return View(_db.CartItems.ToList());
        }

        // ✿ ADD to cart ✿ (ﾉ◕ヮ◕)ﾉ
        [HttpPost]
        public IActionResult Add(int productId)
        {
            var product = _db.Products.Find(productId);
            if (product == null) return RedirectToAction("Index", "Products");

            var item = _db.CartItems.FirstOrDefault(c => c.ProductId == productId);
            if (item == null)
            {
                _db.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;   // already in cart, add one more
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ✿ UPDATE quantity ✿ ٩(◕‿◕)۶
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var item = _db.CartItems.Find(id);
            if (item != null)
            {
                item.Quantity = quantity < 1 ? 1 : quantity;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ✿ REMOVE from cart ✿ (╥﹏╥)
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var item = _db.CartItems.Find(id);
            if (item != null)
            {
                _db.CartItems.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}