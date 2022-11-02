﻿using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private readonly ProductRepository _productRepository;
        public ProductsController(AppDbContext context)
        {
            _productRepository = new ProductRepository();
            _context = context;
            //if (!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product()
            //    {
            //        Name = "Kalem 1",
            //        Price = 100,
            //        Stock = 100,
            //        Color = "Red"
            //    });
            //    _context.Products.Add(new Product()
            //    {
            //        Name = "Kalem 2",
            //        Price = 100,
            //        Stock = 200,
            //        Color = "Red"
            //    });
            //    _context.Products.Add(new Product()
            //    {
            //        Name = "Kalem 3",
            //        Price = 100,
            //        Stock = 300,
            //        Color = "Red"
            //    });
            //    _context.SaveChanges();
            //}
            
        }
        public IActionResult Index()
        {
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
            return View();
        }
        [HttpPost]
        public IActionResult SaveProduct()
        {
            //1.yöntem
            var name = HttpContext.Request.Form["Name"].ToString();
            var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            var color = HttpContext.Request.Form["Color"].ToString();

            Product newProduct = new Product()
            {
                Name = name,
                Price = price,
                Stock = stock,
                Color = color
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            return View();
        }
    }
}