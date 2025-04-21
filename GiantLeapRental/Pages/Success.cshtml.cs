using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using GiantLeapRental.Data;
using Microsoft.AspNetCore.Mvc;
using GiantLeapRental.Models;

namespace GiantLeapRental.Pages
{
    [Authorize]
    public class SuccessModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SuccessModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int bookingId)
        {
            Booking = await _context.Bookings.FindAsync(bookingId);
            if (Booking == null) return RedirectToPage("/Error");

            Booking.DepositPaid = true;
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
