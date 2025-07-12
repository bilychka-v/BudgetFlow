using Application.Extensions;
using Application.Filters;
using Db;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using RazorLight;

namespace Application.Services;

public class PdfGeneratorService
{
    private readonly DataContext _dataContext;
    private readonly RazorLightEngine _razorEngine;
    public PdfGeneratorService(DataContext dataContext)
    {
        _dataContext = dataContext;
        var path = Path.Combine(Directory.GetCurrentDirectory(), "../Application/Tamplates");

        _razorEngine = new RazorLightEngineBuilder().UseFileSystemProject(Path.Combine(path))
            .UseMemoryCachingProvider()
            .Build();
    }
    public async Task<byte[]> GeneratePdfAsync(TransactionFilter filter)
    {
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
        });
        
        var transactions = await _dataContext.Transactions
            .Include(t=> t.Category)
            .Filter(filter)
            .ToListAsync();
        
        var page = await browser.NewPageAsync();

        var htmlContent = await _razorEngine.CompileRenderAsync("Report.cshtml", transactions);
        
        await page.SetContentAsync(htmlContent);
        
        var pdfBytes = await page.PdfDataAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true
        });

        return pdfBytes;
    }
}