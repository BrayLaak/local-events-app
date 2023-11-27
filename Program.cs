using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using local_events_app.Data;
using local_events_app.Services;
using local_events_app.Controllers;

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
            .AddScoped<MeetupDbContext>()
            .AddScoped<MeetupService>()
            .AddScoped<MeetupController>()
            .BuildServiceProvider();

        var baseUrl = configuration["MeetupApi:BaseUrl"];
        var apiKey = configuration["MeetupApi:ApiKey"];

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MeetupDbContext>();
            var meetupService = scope.ServiceProvider.GetRequiredService<MeetupService>();
            var meetupController = scope.ServiceProvider.GetRequiredService<MeetupController>();

            var consoleUI = new ConsoleUI(meetupController, dbContext);

            // Run the console UI
            consoleUI.Run().Wait();
        }
    }
}
