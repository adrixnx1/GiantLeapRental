using Microsoft.AspNetCore.Mvc.RazorPages;
using GiantLeapRental.Data;
using GiantLeapRental.Models;

namespace GiantLeapRental.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Rental> Rentals { get; set; }

        public void OnGet()
        {
            if (!_context.Rentals.Any())
            {
                _context.Rentals.Add(new Rental
                {
                    Name = "Test Castle",
                    Description = "Test item",
                    Category = "Bounce House",
                    ImageUrl = "/images/castle.jpg",
                    PricePerDay = 100
                });
                _context.SaveChanges();
            }

            Rentals = _context.Rentals.ToList();
            Console.WriteLine("📦 Rentals Found: " + Rentals.Count);
            foreach (var r in Rentals)
            {
                Console.WriteLine($"- {r.Name}, ${r.PricePerDay}, Category: {r.Category}");
            }
            }
        }
}

