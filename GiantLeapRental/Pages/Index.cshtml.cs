using Microsoft.AspNetCore.Mvc.RazorPages;
using GiantLeapRental.Models;


public class IndexModel : PageModel
{
    public List<Rental> Rentals { get; set; }

    public void OnGet()
    {
        Rentals = new List<Rental>
        {
            new Rental
            {
                Id = 1,
                Name = "Castle Bounce",
                Description = "Classic colorful bouncy castle.",
                Category = "Bounce House",
                ImageUrl = "/images/castle.jpg",
                PricePerDay = 100
            },
            new Rental
            {
                Id = 2,
                Name = "Jungle Adventure",
                Description = "Bounce into the jungle with this wild setup.",
                Category = "Obstacle + Bounce House",
                ImageUrl = "/images/jungle.jpg",
                PricePerDay = 120
            }
        };
    }
}
