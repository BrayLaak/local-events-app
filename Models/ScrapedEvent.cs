using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace local_events_app.Models
{
    public class ScrapedEvent
    {
        [Key]
        public string Url { get; set; }

        public string Title { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        // Other properties
    }
}
