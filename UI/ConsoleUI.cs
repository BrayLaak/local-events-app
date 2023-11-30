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
        private readonly EventService _eventService;

        public ConsoleUI(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task Run()
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
                        Console.Write("Enter the event Title to save: ");
                        var eventTitleToSave = Console.ReadLine();
                        Console.Write("Enter the event Date: ");
                        var eventDateToSave = Console.ReadLine();
                        Console.Write("Enter the event Description: ");
                        var eventDescriptionToSave = Console.ReadLine();
                        Console.Write("Enter the event Location: ");
                        var eventLocationToSave = Console.ReadLine();

                        var newEvent = new Event
                        {
                            Title = eventTitleToSave,
                            Date = eventDateToSave,
                            Description = eventDescriptionToSave,
                            Location = eventLocationToSave
                        };

                        _eventService.AddEvent(newEvent);
                        Console.WriteLine("Event added successfully!");
                        break;

                    case "2":
                        // Logic to display saved events
                        ViewSavedEvents();
                        break;

                    case "3":
                        // Logic to modify an event
                        Console.Write("Enter the event ID to modify: ");
                        var eventIDToModify = int.Parse(Console.ReadLine());

                        var eventToModify = _eventService.GetEventById(eventIDToModify);

                        if (eventToModify != null)
                        {
                            Console.Write($"Enter new Title for event (current: {eventToModify.Title}): ");
                            eventToModify.Title = Console.ReadLine();

                            Console.Write($"Enter new Date for event (current: {eventToModify.Date}): ");
                            eventToModify.Date = Console.ReadLine();

                            Console.Write($"Enter new Description for event (current: {eventToModify.Description}): ");
                            eventToModify.Description = Console.ReadLine();

                            Console.Write($"Enter new Location for event (current: {eventToModify.Location}): ");
                            eventToModify.Location = Console.ReadLine();

                            _eventService.ModifyEvent(eventIDToModify, eventToModify);
                            Console.WriteLine("Event modified successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Event not found.");
                        }
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

        private void DisplayEvents(List<Event> events)
        {
            // Display events in the console UI
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

        private void ViewSavedEvents()
        {
            // Logic to display saved events
            var savedEvents = _eventService.GetEvents();
            DisplayEvents(savedEvents);
        }

        private void ExportSavedEventsToCsv()
        {
            // Logic to export saved events to CSV
            var savedEvents = _eventService.GetEvents();
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
                Console.WriteLine("No events available. Please add events first.");
            }
        }

        private void DeleteEvent()
        {
            Console.Write("Enter the event ID to delete: ");
            var eventIDToDelete = int.Parse(Console.ReadLine());

            // Use the EventService to delete the event
            _eventService.DeleteEvent(eventIDToDelete);
            Console.WriteLine("Event deleted successfully!");
        }
    }
}
