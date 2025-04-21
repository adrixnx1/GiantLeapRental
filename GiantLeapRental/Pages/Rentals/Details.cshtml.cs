using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GiantLeapRental.Models;

namespace GiantLeapRental.Pages.Rentals
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public string Purpose { get; set; }

        [BindProperty]
        public DateTime RentalDate { get; set; }

        [BindProperty]
        public bool IsTwoDays { get; set; }

        public Rental SelectedRental { get; set; }

        public IActionResult OnGet(int id)
        {
            var rentals = new List<Rental>
            {
                new Rental { Id = 1, Name = "Castle Bounce", Description = "Classic colorful bouncy castle.", Category = "Bounce House", ImageUrl = "/images/castle.jpg", PricePerDay = 100 },
                new Rental { Id = 2, Name = "Jungle Adventure", Description = "Bounce into the jungle!", Category = "Obstacle + Bounce House", ImageUrl = "/images/jungle.jpg", PricePerDay = 120 }
            };

            SelectedRental = rentals.FirstOrDefault(r => r.Id == id);

            if (SelectedRental == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            TempData["RentalName"] = SelectedRental.Name;
            TempData["RentalDate"] = RentalDate.ToShortDateString();
            TempData["IsTwoDays"] = IsTwoDays;
            TempData["Purpose"] = Purpose;
            // TODO: Save booking to database (future step)
            return RedirectToPage("/Confirmation");
        }
    }
}
