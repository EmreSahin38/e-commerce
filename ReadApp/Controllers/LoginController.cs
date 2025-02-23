using Microsoft.AspNetCore.Mvc;
using ReadApp.Models;
using System.Linq;

namespace ReadApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Kullanıcı doğrulandı, yönlendirme yapılabilir
                return RedirectToAction("Index", "Home");
            }

            // Kullanıcı doğrulanamadı, hata mesajı göster
            ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                model.Role = "StandartUsers"; // Yeni kullanıcıların rolü "StandartUsers" olarak ayarlanır
                _context.Users.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}