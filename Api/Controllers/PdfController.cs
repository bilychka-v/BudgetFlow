using Application.Filters;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetFlow.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PdfController : ControllerBase
{
    private readonly IPdfGeneratorService _pdfGeneratorService;
    
    public PdfController(IPdfGeneratorService pdfGeneratorService)
    {
        _pdfGeneratorService = pdfGeneratorService;
    }

    [HttpGet("generate")]
    public async Task<IActionResult> GeneratePdf([FromQuery] TransactionFilter filter)
    {

        var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(filter);

        return File(pdfBytes, "application/pdf", "report.pdf");
    }
}