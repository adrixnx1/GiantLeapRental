namespace GiantLeapRental.Model
{
    public class Rental
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PriceForTwoDays => PricePerDay * 1.75m; // Discount for 2 days
    }
}
