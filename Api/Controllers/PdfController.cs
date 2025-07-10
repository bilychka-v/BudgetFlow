using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetFlow.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PdfController:ControllerBase
{
    private readonly PdfGeneratorService _pdfGeneratorService;
    
    public PdfController(PdfGeneratorService pdfGeneratorService)
    {
        _pdfGeneratorService = pdfGeneratorService;
    }

    [HttpGet("generate")]
    public async Task<IActionResult> GeneratePdf()
    {
        var html = @"
            <html>
                <head>
                    <style>
                        body { font-family: Arial; }
                        h1 { color: #336699; }
                    </style>
                </head>
                <body>
                    <h1>BudgetFlow Звіт</h1>
                    <p> інформація про транзакції, категорії, суми витрат.</p>
                </body>
            </html>";

        var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(html);

        return File(pdfBytes, "application/pdf", "report.pdf");
    }
}