using _01.MVC_Intro_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace _01.MVC_Intro_Demo.Controllers
{
    public class ProductsController : Controller
    {
        private IEnumerable<ProductsViewModel> products =
             new List<ProductsViewModel>()
             {
                new ProductsViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00
                },
                new ProductsViewModel()
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 5.50
                },
                new ProductsViewModel()
                {
                    Id = 3,
                    Name = "Bread",
                    Price = 1.50
                }
             };

        [ActionName("My-Products")]
        public IActionResult All(string keyword)
        {
            if (keyword != null)
            {
                var foundProducts = this.products
                    .Where(pr => pr.Name.ToLower()
                    .Contains(keyword.ToLower()));
                return View(foundProducts);
            }

            return View(this.products);
        }

        public IActionResult ById(int id)
        {
            var product = this.products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return BadRequest();
            }

            return View(product);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return Json(products, options);
        }

        public IActionResult AllAsText()
        {
            //var text = String.Empty;
            //foreach (var pr in products)
            //{
            //    text += $"Products {pr.Id}: {pr.Name} - {pr.Price}lv.";
            //    text += "\r\n";
            //}

            var sb = new StringBuilder();

            foreach (var pr in products)
            {
                sb.AppendLine($"Products {pr.Id}: {pr.Name} - {pr.Price}lv.");
            }

            return Content(sb.ToString().TrimEnd());
        }
    }
}
