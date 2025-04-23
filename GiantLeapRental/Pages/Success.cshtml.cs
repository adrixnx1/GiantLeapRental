using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GiantLeapRental.Data;
using GiantLeapRental.Models;

namespace GiantLeapRental.Pages
{
    public class SuccessModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SuccessModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Booking Booking { get; set; } // ✅ Add this so @Model.Booking works in the .cshtml file

        public async Task<IActionResult> OnGetAsync(int bookingId)
        {
            Booking = await _context.Bookings.FindAsync(bookingId);
            if (Booking == null)
                return NotFound();

            if (!Booking.DepositPaid)
            {
                Booking.DepositPaid = true;
                Booking.IsConfirmed = true;
                await _context.SaveChangesAsync();
            }

            return Page();
        }
    }
}
