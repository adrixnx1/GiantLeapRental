using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using GiantLeapRental.Data;
using GiantLeapRental.Models;
using Stripe.Checkout;

namespace GiantLeapRental.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CheckoutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null || booking.DepositPaid)
            {
                return RedirectToPage("/Error");
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
                            UnitAmount = 7500, // $75.00 in cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Deposit for {booking.RentalName}",
                                Description = $"{booking.RentalDate.ToShortDateString()} - {booking.Purpose}"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = $"{domain}/Success?bookingId={booking.Id}",
                CancelUrl = $"{domain}/"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }
    }
}

