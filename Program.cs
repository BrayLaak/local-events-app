using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using local_events_app.Data;
using local_events_app.Services;
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
            .AddScoped<EventService>()
            .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var eventService = scope.ServiceProvider.GetRequiredService<EventService>(); // Retrieve EventService

            // Run the console UI with the scraper service, event service, and db context
            var consoleUI = new ConsoleUI(eventService);
            consoleUI.Run().Wait();
        }
    }
}
