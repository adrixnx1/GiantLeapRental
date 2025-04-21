using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiantLeapRental.Pages
{
    public class DepositModel : PageModel
    {
        public decimal DepositAmount => 25.00m; // Or whatever you decid
        public void OnGet()
        {
        }
    }
}
