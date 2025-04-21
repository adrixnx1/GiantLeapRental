using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GiantLeapRental.Pages
{
    public class DepositModel : PageModel
    {
        public decimal DepositAmount => 75.00m; // Or whatever you decide
        public void OnGet()
        {
        }
    }
}
