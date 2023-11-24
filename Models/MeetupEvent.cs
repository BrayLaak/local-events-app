using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace local_events_app.Models
{
    public class MeetupEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public string Url { get; set; }
        public MeetupVenue Venue { get; set; }
        public MeetupGroup Group { get; set; }
        // Other properties based on Meetup.com API response tbd

        public class MeetupVenue
        {
            public string Name { get; set; }
            public string Address { get; set; }
            // Will add other venue properties if needed
        }

        public class MeetupGroup
        {
            public string Id { get; set; }
            public string Name { get; set; }
            // Will add other group properties if needed
        }
    }
}
