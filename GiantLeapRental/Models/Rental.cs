namespace GiantLeapRental.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PriceForTwoDays => PricePerDay * 1.75m;
        public string Category { get; set; }  // Example: "Water Slide + Bounce House"

    }
}
