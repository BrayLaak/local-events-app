using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Logging;
using local_events_app.Models;
using local_events_app.Services;

namespace local_events_app.UI
{
    public class ConsoleUI
    {
        private readonly EventService _eventService;
        private readonly ILogger<ConsoleUI> _logger;

        public ConsoleUI(EventService eventService, ILogger<ConsoleUI> logger)
        {
            _eventService = eventService;
            _logger = logger;
        }

        public async Task Run()
        {
            try
            {
                List<Event> events = null; // Initialize events

                while (true)
                {
                    Console.WriteLine("1. Add Event");
                    Console.WriteLine("2. Display Events");
                    Console.WriteLine("3. Modify Event");
                    Console.WriteLine("4. Delete Event");
                    Console.WriteLine("5. Export Saved Events to CSV");
                    Console.WriteLine("6. Exit");

                    Console.Write("Enter your choice: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            // Logic to add an event
                            AddEvent();
                            break;

                        case "2":
                            // Logic to display saved events
                            ViewSavedEvents();
                            break;

                        case "3":
                            // Logic to modify an event
                            ModifyEvent();
                            break;

                        case "4":
                            // Logic to delete an event
                            DeleteEvent();
                            break;

                        case "5":
                            // Logic to export saved events to CSV
                            ExportSavedEventsToCsv();
                            break;

                        case "6":
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                _logger.LogError($"StackTrace: {ex.StackTrace}");
            }
        }

        private void AddEvent()
        {
            try
            {
                Console.Write("Enter the event Title to save (format: text): ");
                var eventTitleToSave = Console.ReadLine();

                Console.Write("Enter the event Date (format: DD-MM-YYYY): ");
                var eventDateToSave = Console.ReadLine();

                Console.Write("Enter the event Description (format: text): ");
                var eventDescriptionToSave = Console.ReadLine();

                Console.Write("Enter the event Location (format: text): ");
                var eventLocationToSave = Console.ReadLine();

                var newEvent = new Event
                {
                    Title = eventTitleToSave,
                    Date = eventDateToSave,
                    Description = eventDescriptionToSave,
                    Location = eventLocationToSave
                };

                _eventService.AddEvent(newEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding event: {ex.Message}");
                _logger.LogError($"StackTrace: {ex.StackTrace}");
            }
        }

        private void ViewSavedEvents()
        {
            try
            {
                var savedEvents = _eventService.GetEvents();
                DisplayEvents(savedEvents);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error displaying events: {ex.Message}");
                _logger.LogError($"StackTrace: {ex.StackTrace}");
            }
        }

        private void ModifyEvent()
        {
            try
            {
                Console.Write("Enter the event ID to modify: ");
                var eventIDToModify = int.Parse(Console.ReadLine());

                var eventToModify = _eventService.GetEventById(eventIDToModify);

                if (eventToModify != null)
                {
                    Console.Write($"Enter new Title for event (current: {eventToModify.Title}, format: text): ");
                    eventToModify.Title = Console.ReadLine();

                    Console.Write($"Enter new Date for event (current: {eventToModify.Date}, format: DD-MM-YYYY): ");
                    eventToModify.Date = Console.ReadLine();

                    Console.Write($"Enter new Description for event (current: {eventToModify.Description}, format: text): ");
                    eventToModify.Description = Console.ReadLine();

                    Console.Write($"Enter new Location for event (current: {eventToModify.Location}, format: text): ");
                    eventToModify.Location = Console.ReadLine();

                    _eventService.ModifyEvent(eventIDToModify, eventToModify);
                }
                else
                {
                    Console.WriteLine("Event not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error modifying event: {ex.Message}");
                _logger.LogError($"StackTrace: {ex.StackTrace}");
            }
        }

        private void DeleteEvent()
        {
            try
            {
                Console.Write("Enter the event ID to delete: ");
                var eventIDToDelete = int.Parse(Console.ReadLine());

                _eventService.DeleteEvent(eventIDToDelete);
                Console.WriteLine("Event deleted successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting event: {ex.Message}");
                _logger.LogError($"StackTrace: {ex.StackTrace}");
            }
        }

        private void DisplayEvents(List<Event> events)
        {
            foreach (var Event in events)
            {
                Console.WriteLine($"ID: {Event.ID}");
                Console.WriteLine($"Title: {Event.Title}");
                Console.WriteLine($"Date: {Event.Date}");
                Console.WriteLine($"Description: {Event.Description}");
                Console.WriteLine($"Location: {Event.Location}");
                Console.WriteLine();
            }
        }

        private void ExportSavedEventsToCsv()
        {
            try
            {
                var savedEvents = _eventService.GetEvents();
                if (savedEvents != null)
                {
                    string projectDirectory = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                    string exportFolderPath = Path.GetFullPath(Path.Combine(projectDirectory, "..", "..", "..", "EventExports"));

                    if (!Directory.Exists(exportFolderPath))
                    {
                        Directory.CreateDirectory(exportFolderPath);
                    }

                    string fullPath = Path.Combine(exportFolderPath, "saved_events.csv");

                    using (var writer = new StreamWriter(fullPath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(savedEvents);
                    }

                    Console.WriteLine($"Saved events exported to '{fullPath}'");
                }
                else
                {
                    Console.WriteLine("No events available. Please add events first.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error exporting events to CSV: {ex.Message}");
                _logger.LogError($"StackTrace: {ex.StackTrace}");
            }
        }
    }
}
