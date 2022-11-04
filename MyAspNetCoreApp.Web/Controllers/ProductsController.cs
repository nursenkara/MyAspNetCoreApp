using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly ProductRepository _productRepository;
        private IHelper _helper;
        public ProductsController(AppDbContext context, IHelper helper)
        {
            _productRepository = new ProductRepository();
            _context = context;
            _helper = helper;
        }
        public IActionResult Index()
        {
            var text = "Asp.Net";
            var upperText = _helper.Upper(text);

            var products = _context.Products.ToList();

            return View(products);
        }
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
         
        [HttpGet]
        public IActionResult Add()
        {
            Dictionary<string, int> Expire = new Dictionary<string, int>()
            {
                {"1 Ay", 1 },
                {"3 Ay", 3 },
                {"6 Ay", 6 },
                {"12 Ay", 12 },
            };
            ViewBag.Expire = Expire;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product newProduct)
        {
            //1.yöntem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();
            //2.yöntem
            //Product newProduct = new Product()
            //{
            //    Name = Name,
            //    Price = Price,
            //    Stock = Stock,
            //    Color = Color
            //};
            TempData["status"] = "Ürün başarıyla eklendi";
            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId)
        {
            updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
