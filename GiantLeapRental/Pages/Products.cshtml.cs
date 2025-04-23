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
            Rentals = _context.Rentals.ToList();
        }
    }
}

