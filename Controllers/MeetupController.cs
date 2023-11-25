using local_events_app.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace local_events_app.Controllers
{
    public class MeetupController
    {
        private readonly MeetupService _meetupService;

        public MeetupController(MeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        // Methods to handle Meetup API actions
        public async Task SearchLocalEventsAsync(string location)
        {
            var meetupEvents = await _meetupService.SearchLocalEventsAsync(location);

            // Handle and display Meetup events in the console UI
            // ...
        }
    }
}
