using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace GiantLeapRental.Pages.Admin
{
    public class ExportPdfModel : PageModel
    {
        public IActionResult OnGet()
        {
            var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.Content().Text("🚀 Hello from GiantLeapRental PDF!").FontSize(24).Bold();
                });
            })
            .GeneratePdf(stream);

            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", "test-booking.pdf");
        }
    }
}
