using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using local_events_app.Data;
using local_events_app.Models;
using local_events_app.Services;

namespace local_events_app.UI
{
    public class ConsoleUI
    {
        private readonly LexingtonGovScraperService _scraperService;
        private readonly EventService _eventService;
        private readonly AppDbContext _dbContext;
        private readonly List<ScrapedEvent> _scrapedEvents;

        public ConsoleUI(LexingtonGovScraperService scraperService, EventService eventService, AppDbContext dbContext)
        {
            _scraperService = scraperService;
            _eventService = eventService;
            _dbContext = dbContext;
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
                Console.WriteLine("5. View Existing Events");
                Console.WriteLine("6. Delete Event");
                Console.WriteLine("7. Exit");

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
                        DisplayEvents(events);
                        break;

                    case "2":
                        // Logic to display saved events
                        ViewSavedEvents();
                        break;

                    case "3":
                        // Logic to save an event
                        SaveEvent();
                        break;

                    case "4":
                        // Logic to export saved events to CSV
                        ExportSavedEventsToCsv();
                        break;

                    case "5":
                        // Logic to view existing events
                        ViewExistingEvents();
                        break;

                    case "6":
                        // Logic to delete an event
                        DeleteEvent();
                        break;

                    case "7":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void DisplayEvents(List<ScrapedEvent> events)
        {
            // Display events in the console UI
            foreach (var scrapedEvent in events)
            {
                Console.WriteLine($"Title: {scrapedEvent.Title}, Date: {scrapedEvent.Date}");
                Console.WriteLine($"Description: {scrapedEvent.Description}");
                Console.WriteLine($"Location: {scrapedEvent.Location}");
                Console.WriteLine($"URL: {scrapedEvent.Url}");
                Console.WriteLine();
            }
        }

        private void ViewSavedEvents()
        {
            // Logic to display saved events
            var savedEvents = _eventService.GetSavedEvents();
            DisplayEvents(savedEvents);
        }

        private void SaveEvent()
        {
            Console.Write("Enter the event Title to save: ");
            var eventTitleToSave = Console.ReadLine();

            var eventToSave = _scrapedEvents?.FirstOrDefault(e => e.Title == eventTitleToSave);

            if (eventToSave != null)
            {
                // Use the EventService to save the event
                _eventService.SaveEvent(eventToSave);
                Console.WriteLine("Event saved successfully!");
            }
            else
            {
                Console.WriteLine("Invalid event Title. No event saved.");
            }
        }

        private void ExportSavedEventsToCsv()
        {
            // Logic to export saved events to CSV
            var savedEvents = _eventService.GetSavedEvents();
            if (savedEvents != null)
            {
                using (var writer = new StreamWriter("saved_events.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(savedEvents);
                }

                Console.WriteLine("Saved events exported to 'saved_events.csv'");
            }
            else
            {
                Console.WriteLine("No events available. Please search for events first.");
            }
        }

        private void ViewExistingEvents()
        {
            // Use the EventService to get and display saved events
            var savedEvents = _eventService.GetSavedEvents();
            DisplayEvents(savedEvents);
        }

        private void DeleteEvent()
        {
            Console.Write("Enter the event Title to delete: ");
            var eventTitleToDelete = Console.ReadLine();

            // Use the EventService to delete the event
            _eventService.DeleteEvent(eventTitleToDelete);
            Console.WriteLine("Event deleted successfully!");
        }
    }
}
