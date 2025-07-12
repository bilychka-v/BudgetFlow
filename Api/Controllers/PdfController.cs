using Application.Filters;
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
    public async Task<IActionResult> GeneratePdf([FromQuery] TransactionFilter filter)
    {

        var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(filter);

        return File(pdfBytes, "application/pdf", "report.pdf");
    }
}