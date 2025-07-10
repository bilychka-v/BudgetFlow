using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace Application.Services;

public class PdfGeneratorService
{
    public async Task<byte[]> GeneratePdfAsync(string htmlContent)
    {
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            ExecutablePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"
        });
        
        await using var page = await browser.NewPageAsync();
        
        await page.SetContentAsync(htmlContent);
        
        var pdfBytes = await page.PdfDataAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true
        });

        return pdfBytes;
    }
}