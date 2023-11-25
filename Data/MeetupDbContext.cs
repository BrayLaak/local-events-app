using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using local_events_app.Models;

namespace local_events_app.Data
{

    public class MeetupDbContext : DbContext
    {
        public DbSet<SavedMeetupEvent> SavedMeetupEvents { get; set; }

        // Constructor with DbContextOptions, required by migrations and OnConfiguring
        public MeetupDbContext(DbContextOptions<MeetupDbContext> options) : base(options)
        {
        }

        // Empty constructor for your application's runtime
        public MeetupDbContext()
        {
        }

        // Configure the context to use SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // UseSqlite method sets the database provider to SQLite
            optionsBuilder.UseSqlite("Data Source=meetup.db");
        }

        // Additional configuration for the model if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the model if needed
        }
    }
}