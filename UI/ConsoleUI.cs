using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
//using local_events_app.Controllers;
using local_events_app.Data;
using local_events_app.Models;
using local_events_app.Services;

namespace local_events_app.UI
{
    public class ConsoleUI
    {
        private readonly LexingtonGovScraperService _scraperService;
        private readonly AppDbContext _dbContext;
        private readonly List<ScrapedEvent> _scrapedEvents;

        public ConsoleUI(LexingtonGovScraperService scraperService, AppDbContext dbContext, List<ScrapedEvent> scrapedEvents)
        {
            _scraperService = scraperService;
            _dbContext = dbContext;
            _scrapedEvents = scrapedEvents;
        }

        public async Task Run()
        {
            List<ScrapedEvent> events = null; // Initialize events

            while (true)
            {
                Console.WriteLine("1. Search Local Events");
                Console.WriteLine("2. Display Saved Events");
                Console.WriteLine("3. Save Event");
                Console.WriteLine("4. Export Saved Events to CSV");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter location: ");
                        var location = Console.ReadLine();

                        // Use scraper service to get events
                        events = await _scraperService.ScrapeEventsAsync();

                        // Process and display events in the console UI
                        foreach (var scrapedEvent in events)
                        {
                            Console.WriteLine($"Title: {scrapedEvent.Title}, Date: {scrapedEvent.Date}");
                            Console.WriteLine($"Description: {scrapedEvent.Description}");
                            Console.WriteLine($"Location: {scrapedEvent.Location}");
                            Console.WriteLine($"URL: {scrapedEvent.Url}");
                            Console.WriteLine();
                        }
                        break;

                    case "2":
                        // Logic to display saved events
                        if (events != null)
                        {
                            foreach (var savedEvent in events)
                            {
                                Console.WriteLine($"Title: {savedEvent.Title}, Date: {savedEvent.Date}");
                                Console.WriteLine($"Description: {savedEvent.Description}");
                                Console.WriteLine($"Location: {savedEvent.Location}");
                                Console.WriteLine($"URL: {savedEvent.Url}");
                                Console.WriteLine("------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No events available. Please search for events first.");
                        }
                        break;

                    case "3":
                        // Logic to save an event
                        Console.Write("Enter the event Title to save: ");
                        var eventTitleToSave = Console.ReadLine();

                        var eventToSave = events?.FirstOrDefault(e => e.Title == eventTitleToSave);

                        if (eventToSave != null)
                        {
                            // Use the ScrapedEvent entity
                            _dbContext.ScrapedEvents.Add(eventToSave);
                            _dbContext.SaveChanges();

                            Console.WriteLine("Event saved successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid event Title. No event saved.");
                        }
                        break;

                    case "4":
                        // Logic to export saved events to CSV
                        if (events != null)
                        {
                            using (var writer = new StreamWriter("saved_events.csv"))
                            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                            {
                                csv.WriteRecords(events);
                            }

                            Console.WriteLine("Saved events exported to 'saved_events.csv'");
                        }
                        else
                        {
                            Console.WriteLine("No events available. Please search for events first.");
                        }
                        break;

                    case "5":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
