using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace local_events_app.Models
{
    public class SavedMeetupEvent
    {
        public int Id { get; set; }
        public string MeetupEventId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        // Other properties based on saved Meetup event data tbd
    }
}
