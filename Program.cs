using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using local_events_app.Data;
using local_events_app.Services;
//using local_events_app.Controllers;
using local_events_app.UI;

class Program
{
    static void Main()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddScoped<AppDbContext>()
            .AddScoped<LexingtonGovScraperService>()
            .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var scraperService = scope.ServiceProvider.GetRequiredService<LexingtonGovScraperService>();

            // Create an instance of the scraper service
            LexingtonGovScraperService scraper = new LexingtonGovScraperService();

            // Call the ScrapeEventsAsync method to initiate scraping
            var scrapedEvents = scraper.ScrapeEventsAsync().Result;

            // Run the console UI with the scraper service and scraped events
            var consoleUI = new ConsoleUI(scraperService, dbContext, scrapedEvents);
            consoleUI.Run().Wait();
        }
    }
}
