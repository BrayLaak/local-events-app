using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using local_events_app.Controllers;
using local_events_app.Models;
using local_events_app.Services;

public class ConsoleUI
{
    private readonly MeetupController _meetupController;

    public ConsoleUI(MeetupController meetupController)
    {
        _meetupController = meetupController;
    }

    public async Task Run()
    {
        while (true)
        {
            Console.WriteLine("1. Search Local Meetup Events");
            Console.WriteLine("2. Display Saved Meetup Events");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter location: ");
                    var location = Console.ReadLine();
                    await _meetupController.SearchLocalEventsAsync(location);
                    break;

                case "2":
                    // Logic to display saved Meetup events tbd
                    break;

                case "3":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}