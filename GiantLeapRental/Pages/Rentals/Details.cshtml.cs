using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GiantLeapRental.Data;
using GiantLeapRental.Models;
using GiantLeapRental.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GiantLeapRental.Pages.Rentals
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EmailSender _emailSender;

        public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, EmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
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

            // 🧹 Remove expired bookings older than 10 minutes that were never confirmed
            var expired = _context.Bookings
                .Where(b => !b.IsConfirmed && (DateTime.Now - b.CreatedAt).TotalMinutes > 10)
                .ToList();

            if (expired.Any())
            {
                _context.Bookings.RemoveRange(expired);
                _context.SaveChanges();
            }

            // 🗓️ Load only confirmed bookings to block the calendar
            BookedDates = _context.Bookings
                .Where(b => b.RentalName == SelectedRental.Name && b.IsConfirmed)
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

            _emailSender.Send(
                toEmail: user.Email,
                subject: "Your Giant Leap Rental Booking",
                body: $"Thank you for booking **{booking.RentalName}** on {booking.RentalDate:d}.\n\n" +
                      $"Duration: {(booking.IsTwoDays ? "2 Days" : "1 Day")}\n" +
                      $"Purpose: {booking.Purpose}\n\nWe’ll follow up with more info soon!"
            );

            // ✅ Redirect using query param instead of TempData
            return RedirectToPage("/Confirmation", new { bookingId = booking.Id });
        }
    }
}