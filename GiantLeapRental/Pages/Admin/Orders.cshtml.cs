using Microsoft.AspNetCore.Authorization;
using GiantLeapRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiantLeapRental.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class OrdersModel : PageModel
    {
        public List<Booking> Bookings { get; set; }
        public void OnGet()
        {
            Bookings = new List<Booking>
            {
                new Booking { RentalName = "Castle", RentalDate = DateTime.Today }
            };
        }
    }
}
