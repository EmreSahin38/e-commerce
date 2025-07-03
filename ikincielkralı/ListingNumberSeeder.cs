using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ikincielkralı.Models;

namespace ikincielkralı
{
    public static class ListingNumberSeeder
    {
        public static void SeedListingNumbers(AppDbContext context)
        {
            var random = new Random();
            var listings = context.Listings.Where(l => string.IsNullOrEmpty(l.ListingNumber)).ToList();
            foreach (var listing in listings)
            {
                string number;
                do
                {
                    number = string.Concat(Enumerable.Range(0, 10).Select(_ => random.Next(0, 10).ToString()));
                } while (context.Listings.Any(l => l.ListingNumber == number));
                listing.ListingNumber = number;
            }
            context.SaveChanges();
        }
    }
}
