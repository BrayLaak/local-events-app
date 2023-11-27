using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using local_events_app.Controllers;
using local_events_app.Data;
using local_events_app.Models;

public class ConsoleUI
{
    private readonly MeetupController _meetupController;
    private readonly MeetupDbContext _dbContext;

    public ConsoleUI(MeetupController meetupController, MeetupDbContext dbContext)
    {
        _meetupController = meetupController;
        _dbContext = dbContext;
    }

    public async Task Run()
    {
        while (true)
        {
            Console.WriteLine("1. Search Local Meetup Events");
            Console.WriteLine("2. Display Saved Meetup Events");
            Console.WriteLine("3. Save Meetup Event");
            Console.WriteLine("4. Export Saved Events to CSV");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter location: ");
                    var location = Console.ReadLine();
                    var meetupEvents = await _meetupController.SearchLocalEventsAsync(location);

                    // Process and display Meetup events in the console UI
                    foreach (var meetupEvent in meetupEvents)
                    {
                        Console.WriteLine($"Name: {meetupEvent.Name}");
                        Console.WriteLine($"Description: {meetupEvent.Description}");
                        Console.WriteLine($"DateTime: {meetupEvent.Time}");
                        Console.WriteLine("------------------------");
                    }
                    break;

                case "2":
                    // Logic to display saved Meetup events
                    var savedEvents = _dbContext.SavedMeetupEvents.ToList();
                    foreach (var savedEvent in savedEvents)
                    {
                        Console.WriteLine($"Id: {savedEvent.Id}");
                        Console.WriteLine($"Name: {savedEvent.Name}");
                        Console.WriteLine($"Url: {savedEvent.Url}");
                        Console.WriteLine("------------------------");
                    }
                    break;

                case "3":
                    // Logic to save a Meetup event
                    Console.Write("Enter the event Id to save: ");
                    var eventIdToSave = Console.ReadLine();
                    var eventToSave = meetupEvents.FirstOrDefault(e => e.Id == eventIdToSave);

                    if (eventToSave != null)
                    {
                        var savedEvent = new SavedMeetupEvent
                        {
                            MeetupEventId = eventToSave.Id,
                            Name = eventToSave.Name,
                            Url = eventToSave.Url
                            // Add other properties as needed
                        };

                        _dbContext.SavedMeetupEvents.Add(savedEvent);
                        _dbContext.SaveChanges();

                        Console.WriteLine("Event saved successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid event Id. No event saved.");
                    }
                    break;

                case "4":
                    // Logic to export saved events to CSV
                    var csvExport = new CsvExport(_dbContext.SavedMeetupEvents.ToList());
                    csvExport.Export("saved_events.csv");
                    Console.WriteLine("Saved events exported to 'saved_events.csv'");
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
