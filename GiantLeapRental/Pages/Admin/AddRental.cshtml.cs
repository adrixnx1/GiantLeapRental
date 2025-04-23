using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GiantLeapRental.Data;
using GiantLeapRental.Models;
using Microsoft.AspNetCore.Authorization;

namespace GiantLeapRental.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddRentalModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddRentalModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rental NewRental { get; set; } = new Rental();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("⚠️ Form submission invalid.");
                return Page();
            }

            Console.WriteLine("✅ Rental submitted: " + NewRental.Name);

            _context.Rentals.Add(NewRental);
            await _context.SaveChangesAsync();

            Console.WriteLine("✅ Rental saved to database.");
            return RedirectToPage("/Products");
        }

    }
}
