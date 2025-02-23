using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReadApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository _repository;

        public HomeController(Repository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string searchString, string category)
        {
            var products = _repository.Products;

            if (!string.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(category) && category != "0")
            {
                products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
            }

            var viewModel = new ProductViewModel
            {
                Products = products,
                SearchString = searchString,
                Category = category,
                Categories = _repository.Categories ?? new List<Category>(),
                SelectedCategory = !string.IsNullOrEmpty(category) ? int.Parse(category) : (int?)null
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return Unauthorized();
            }

            ViewBag.Categories = new SelectList(_repository.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (!IsAdmin())
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                _repository.CreateProduct(model);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_repository.Categories, "CategoryId", "Name");
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
            {
                return Unauthorized();
            }

            var product = _repository.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_repository.Categories, "CategoryId", "Name");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (!IsAdmin())
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                _repository.EditProduct(model);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(_repository.Categories, "CategoryId", "Name");
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
            {
                return Unauthorized();
            }

            var product = _repository.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _repository.DeleteProduct(product);
            return RedirectToAction("Index");
        }

        private bool IsAdmin()
        {
            var username = User.Identity?.Name;
            var user = _repository.Users.FirstOrDefault(u => u.Username == username);
            return user != null && user.Role == "Admin";
        }
    }
}