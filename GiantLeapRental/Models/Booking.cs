using System.ComponentModel.DataAnnotations;

namespace GiantLeapRental.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string RentalName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        public string Purpose { get; set; }

        public bool IsTwoDays { get; set; }

        public bool DepositPaid { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}

