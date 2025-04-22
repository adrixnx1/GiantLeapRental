using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GiantLeapRental.Models;
using GiantLeapRental.Data;

namespace GiantLeapRental.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class OrdersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public OrdersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Booking> Bookings { get; set; }

        public void OnGet()
        {
            Bookings = _context.Bookings
                .OrderBy(b => b.RentalDate)
                .ToList();
        }

        // ✅ Toggle deposit paid status
        public async Task<IActionResult> OnPostToggleDepositAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                booking.DepositPaid = !booking.DepositPaid;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostCancelBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking); // or set booking.IsCanceled = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); // refresh the page
        }

    }
}
