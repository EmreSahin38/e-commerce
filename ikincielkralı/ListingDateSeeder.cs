using System;
using System.Linq;
namespace ikincielkralı
{
    public static class ListingDateSeeder
    {
        public static void SeedListingDates(AppDbContext context)
        {
            var today = DateTime.Now;
            var listings = context.Listings.Where(l => l.CreatedAt == null).ToList();
            foreach (var listing in listings)
            {
                listing.CreatedAt = today;
            }
            context.SaveChanges();
        }
    }
}
