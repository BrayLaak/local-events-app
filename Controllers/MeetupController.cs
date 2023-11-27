using System.Collections.Generic;
using System.Threading.Tasks;
using local_events_app.Models;
using local_events_app.Services;

namespace local_events_app.Controllers
{
    public class MeetupController
    {
        private readonly MeetupService _meetupService;

        public MeetupController(MeetupService meetupService)
        {
            _meetupService = meetupService;
        }

        public async Task<List<MeetupEvent>> SearchLocalEventsAsync(string location)
        {
            return await _meetupService.SearchLocalEventsAsync(location);
        }
    }
}
