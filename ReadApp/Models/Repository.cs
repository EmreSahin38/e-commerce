#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReadApp.Models
{
    public class Repository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> Products => _context.Products.ToList();
        public List<Category> Categories => _context.Categories.ToList();
        public List<User> Users => _context.Users.ToList(); // Kullanıcılar için DbSet eklendi

        public void CreateProduct(Product entity)
        {
            // En küçük boş ProductId'yi bul
            var nextId = 1;
            if (_context.Products.Any())
            {
                var maxId = _context.Products.Max(p => p.ProductId);
                var usedIds = _context.Products.Select(p => p.ProductId).ToHashSet();
                for (int i = 1; i <= maxId; i++)
                {
                    if (!usedIds.Contains(i))
                    {
                        nextId = i;
                        break;
                    }
                }
                if (nextId == 1 && usedIds.Contains(1))
                {
                    nextId = maxId + 1;
                }
            }
            entity.ProductId = nextId;

            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void EditProduct(Product updateProduct)
        {
            var entity = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == updateProduct.ProductId);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            _context.ChangeTracker.Clear();

            _context.Products.Update(updateProduct);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product entity)
        {
            var prdEntity = _context.Products.FirstOrDefault(p => p.ProductId == entity.ProductId);
            if (prdEntity != null)
            {
                _context.Products.Remove(prdEntity);
                _context.SaveChanges();
            }
        }

        public void DeleteUser(User entity)
        {
            if (entity.Username == "Admin")
            {
                throw new InvalidOperationException("Admin kullanıcısı silinemez.");
            }

            var userEntity = _context.Users.FirstOrDefault(u => u.UserId == entity.UserId);
            if (userEntity != null)
            {
                _context.Users.Remove(userEntity);
                _context.SaveChanges();
            }
        }
    }
}