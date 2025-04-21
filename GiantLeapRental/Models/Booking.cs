namespace GiantLeapRental.Models
{
    public class Booking
    {
        public string RentalName { get; set; }
        public string Date { get; set; }
        public string Purpose { get; set; }
        public string Duration => IsTwoDays ? "2 Days" : "1 Day";
        public bool IsTwoDays { get; set; }
        public bool DepositPaid { get; set; } // future use
    }
}
