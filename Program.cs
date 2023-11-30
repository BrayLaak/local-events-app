using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using local_events_app.Data;
using local_events_app.Services;
using local_events_app.UI;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        // Configure Serilog to write logs to the console and a file
        string projectDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
        string logFolderPath = Path.GetFullPath(Path.Combine(projectDirectory, "..", "..", "..", "Logs/log.txt"));

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(logFolderPath, rollingInterval: RollingInterval.Day)
            .CreateLogger();


        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddScoped<AppDbContext>()
            .AddScoped<EventService>()
            .AddScoped<ConsoleUI>()
            .AddLogging(builder =>
            {
                builder.AddSerilog();
                builder.SetMinimumLevel(LogLevel.Error);
            })
            .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during database migration: {ex.Message}");
            }

            var eventService = scope.ServiceProvider.GetRequiredService<EventService>();
            var consoleUI = scope.ServiceProvider.GetRequiredService<ConsoleUI>();
            consoleUI.Run().Wait();
        }

        // Close and flush the log when the application exits
        Log.CloseAndFlush();
    }
}
