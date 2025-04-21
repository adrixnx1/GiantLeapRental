using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using GiantLeapRental.Data;
using GiantLeapRental.Models;
using Microsoft.AspNetCore.Identity;

namespace GiantLeapRental.Pages.Bookings
{
    [Authorize]
    public class MyBookingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MyBookingsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Booking> UserBookings { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var email = user.Email;

            UserBookings = _context.Bookings
                .Where(b => b.UserEmail == email)
                .OrderBy(b => b.RentalDate)
                .ToList();
        }
    }
}

