using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace local_events_app.Data
{
    public class MeetupDbContext : DbContext
    {
        public DbSet<SavedMeetupEvent> SavedMeetupEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=meetup.db");
        }
    }
}
