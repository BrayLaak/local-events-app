using local_events_app.Models;
using Microsoft.EntityFrameworkCore;

namespace local_events_app.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ScrapedEvent> ScrapedEvents { get; set; }

        // Constructor with DbContextOptions, required by migrations and OnConfiguring
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Empty constructor for your application's runtime
        public AppDbContext()
        {
        }

        // Configure the context to use SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // UseSqlite method sets the database provider to SQLite
            optionsBuilder.UseSqlite("Data Source=events.db");
        }

        // Additional configuration for the model if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the model if needed

            // Set Url property as the key for ScrapedEvent
            modelBuilder.Entity<ScrapedEvent>().HasKey(e => e.Url);
        }
    }
}
