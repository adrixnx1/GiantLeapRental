using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using GiantLeapRental.Data;
using GiantLeapRental.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> OnGetAsync()
        {
            Bookings = await _context.Bookings
                .OrderBy(b => b.RentalDate)
                .ToListAsync();

            return Page();
        }

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
                _context.Bookings.Remove(booking); // or use booking.IsCanceled = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
