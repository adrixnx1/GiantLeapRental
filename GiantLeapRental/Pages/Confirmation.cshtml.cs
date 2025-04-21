using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiantLeapRental.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string RentalName { get; set; }
        public string RentalDate { get; set; }
        public string Purpose { get; set; }
        public bool IsTwoDays { get; set; }

        public void OnGet()
        {
            RentalName = TempData["RentalName"]?.ToString();
            RentalDate = TempData["RentalDate"]?.ToString();
            Purpose = TempData["Purpose"]?.ToString();
            IsTwoDays = TempData["IsTwoDays"] != null && bool.Parse(TempData["IsTwoDays"].ToString());
        }
    }
}