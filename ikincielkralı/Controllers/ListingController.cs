using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ikincielkralı.Models;
using System.IO;
using System.Linq;
using System.Security.Claims;

namespace ikincielkralı.Controllers
{
    public class ListingController : Controller
    {
        private readonly AppDbContext _context;
        public ListingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string q, string condition, string damageStatus, decimal? minPrice, decimal? maxPrice, string search)
        {
            var listings = _context.Listings
                .Include(l => l.User) // Kullanıcı bilgisi için eklendi
                .AsQueryable();
            if (!string.IsNullOrEmpty(q))
                listings = listings.Where(l => l.Category == q);
            if (!string.IsNullOrEmpty(condition))
                listings = listings.Where(l => l.Condition == condition);
            if (!string.IsNullOrEmpty(damageStatus))
                listings = listings.Where(l => l.DamageStatus == damageStatus);
            if (minPrice.HasValue)
                listings = listings.Where(l => l.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                listings = listings.Where(l => l.Price <= maxPrice.Value);
            if (!string.IsNullOrEmpty(search))
            {
                listings = listings.Where(l =>
                    l.Description.Contains(search) ||
                    l.Category.Contains(search) ||
                    l.Condition.Contains(search) ||
                    l.DamageStatus.Contains(search) ||
                    (l.CarBrand != null && l.CarBrand.Contains(search)) ||
                    (l.CarModel != null && l.CarModel.Contains(search)) ||
                    (l.HouseCity != null && l.HouseCity.Contains(search)) ||
                    (l.HouseDistrict != null && l.HouseDistrict.Contains(search)) ||
                    (l.User != null && l.User.Username.Contains(search))
                );
            }
            var result = listings.ToList();
            return View(result);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Models.ListingCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                int userId = int.Parse(userIdClaim.Value);
                var photoPaths = new List<string>();
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    if (model.Photos.Count > 20)
                    {
                        ModelState.AddModelError("Photos", "En fazla 20 fotoğraf yükleyebilirsiniz.");
                        return View(model);
                    }
                    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsPath))
                        Directory.CreateDirectory(uploadsPath);
                    foreach (var photo in model.Photos)
                    {
                        if (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png")
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                            var filePath = Path.Combine(uploadsPath, fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                photo.CopyTo(stream);
                            }
                            photoPaths.Add("/uploads/" + fileName);
                        }
                    }
                }
                if (model.Photos == null || model.Photos.Count == 0)
                {
                    ModelState.AddModelError("Photos", "En az bir fotoğraf yüklemelisiniz.");
                    return View(model);
                }
                var random = new Random();
                string listingNumber;
                do
                {
                    listingNumber = string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()));
                } while (_context.Listings.Any(l => l.ListingNumber == listingNumber));
                var listing = new Listing
                {
                    Category = model.Category,
                    Condition = model.Condition,
                    DamageStatus = model.DamageStatus,
                    Description = model.Description,
                    Price = model.Price,
                    UserId = userId,
                    ListingNumber = listingNumber,
                    CreatedAt = DateTime.Now,
                    PhotoPaths = photoPaths,
                    CarBrand = model.CarBrand,
                    CarModel = model.CarModel,
                    CarYear = model.CarYear,
                    CarKm = model.CarKm,
                    HouseCity = model.HouseCity,
                    HouseDistrict = model.HouseDistrict
                };
                _context.Listings.Add(listing);
                _context.SaveChanges();
                TempData["Success"] = "İlan başarıyla oluşturuldu.";
                return RedirectToAction("MyListings");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult MyListings()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var listings = _context.Listings.Where(l => l.UserId == userId).ToList();
            return View(listings);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var listing = _context.Listings.FirstOrDefault(l => l.Id == id && l.UserId == userId);
            if (listing == null) return NotFound();
            var model = new Models.ListingCreateViewModel
            {
                Category = listing.Category,
                Condition = listing.Condition,
                DamageStatus = listing.DamageStatus,
                Description = listing.Description,
                Price = listing.Price
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, Models.ListingCreateViewModel model)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var listing = _context.Listings.FirstOrDefault(l => l.Id == id && l.UserId == userId);
            if (listing == null) return NotFound();
            if (ModelState.IsValid)
            {
                listing.Category = model.Category;
                listing.Condition = model.Condition;
                listing.DamageStatus = model.DamageStatus;
                listing.Description = model.Description;
                listing.Price = model.Price;

                // Yeni alanlar (araba/ev)
                listing.CarBrand = model.CarBrand;
                listing.CarModel = model.CarModel;
                listing.CarYear = model.CarYear;
                listing.CarKm = model.CarKm;
                listing.HouseCity = model.HouseCity;
                listing.HouseDistrict = model.HouseDistrict;

                // Çoklu fotoğraf güncelleme
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    if (model.Photos.Count > 20)
                    {
                        ModelState.AddModelError("Photos", "En fazla 20 fotoğraf yükleyebilirsiniz.");
                        return View(model);
                    }
                    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsPath))
                        Directory.CreateDirectory(uploadsPath);
                    var photoPaths = new List<string>();
                    foreach (var photo in model.Photos)
                    {
                        if (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png")
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                            var filePath = Path.Combine(uploadsPath, fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                photo.CopyTo(stream);
                            }
                            photoPaths.Add("/uploads/" + fileName);
                        }
                    }
                    listing.PhotoPaths = photoPaths;
                }
                // Fotoğraf yüklenmezse eski fotoğraflar korunur, zorunlu değil

                _context.SaveChanges();
                TempData["Success"] = "İlan güncellendi.";
                return RedirectToAction("MyListings");
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = int.Parse(userIdClaim.Value);
            var listing = _context.Listings.FirstOrDefault(l => l.Id == id && l.UserId == userId);
            if (listing == null) return NotFound();
            _context.Listings.Remove(listing);
            _context.SaveChanges();
            TempData["Success"] = "İlan silindi.";
            return RedirectToAction("MyListings");
        }

        public IActionResult Details(int id)
        {
            var listing = _context.Listings.FirstOrDefault(l => l.Id == id);
            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }
    }
}
