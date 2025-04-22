using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GiantLeapRental.Data;
using GiantLeapRental.Models;
using Stripe.Checkout;

namespace GiantLeapRental.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CheckoutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            var domain = $"{Request.Scheme}://{Request.Host}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = 7500, // $75 deposit
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Deposit for {booking.RentalName} on {booking.RentalDate.ToShortDateString()}",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = $"{domain}/Success?bookingId={booking.Id}",
                CancelUrl = $"{domain}/Confirmation?bookingId={booking.Id}"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            // ✅ Confirm the booking now that payment session has been created (optional — or do it in /Success)
            // You may prefer to do this in Success.cshtml.cs instead!
            // booking.IsConfirmed = true;
            // await _context.SaveChangesAsync();

            return Redirect(session.Url);
        }
    }
}

