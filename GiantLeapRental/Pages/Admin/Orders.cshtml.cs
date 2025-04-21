using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
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
            Bookings = _context.Bookings.ToList();
        }
    }
}

