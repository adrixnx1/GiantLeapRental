using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GiantLeapRental.Data;
using GiantLeapRental.Models;

namespace GiantLeapRental.Pages.Rentals
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Rental SelectedRental { get; set; }

        [BindProperty]
        public string Purpose { get; set; }

        [BindProperty]
        public DateTime RentalDate { get; set; }

        [BindProperty]
        public bool IsTwoDays { get; set; }

        public List<DateTime> BookedDates { get; set; }

        public IActionResult OnGet(int id)
        {
            var rentals = new List<Rental>
            {
                new Rental { Id = 1, Name = "Castle Bounce", Description = "Classic colorful bouncy castle.", Category = "Bounce House", ImageUrl = "/images/castle.jpg", PricePerDay = 100 },
                new Rental { Id = 2, Name = "Jungle Adventure", Description = "Bounce into the jungle!", Category = "Obstacle + Bounce House", ImageUrl = "/images/jungle.jpg", PricePerDay = 120 }
            };

            SelectedRental = rentals.FirstOrDefault(r => r.Id == id);
            if (SelectedRental == null) return NotFound();

            // Load already booked dates for this rental
            BookedDates = _context.Bookings
                .Where(b => b.RentalName == SelectedRental.Name)
                .Select(b => b.RentalDate)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            var rental = new List<Rental>
            {
                new Rental { Id = 1, Name = "Castle Bounce", Description = "Classic colorful bouncy castle.", Category = "Bounce House", ImageUrl = "/images/castle.jpg", PricePerDay = 100 },
                new Rental { Id = 2, Name = "Jungle Adventure", Description = "Bounce into the jungle!", Category = "Obstacle + Bounce House", ImageUrl = "/images/jungle.jpg", PricePerDay = 120 }
            }.FirstOrDefault(r => r.Id == id);

            if (rental == null) return NotFound();

            var booking = new Booking
            {
                RentalName = rental.Name,
                UserEmail = user.Email,
                RentalDate = RentalDate,
                Purpose = Purpose,
                IsTwoDays = IsTwoDays,
                DepositPaid = false
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            TempData["RentalName"] = booking.RentalName;
            TempData["RentalDate"] = booking.RentalDate.ToShortDateString();
            TempData["IsTwoDays"] = booking.IsTwoDays;
            TempData["Purpose"] = booking.Purpose;

            return RedirectToPage("/Confirmation");
        }
    }
}

