using System.Collections.Generic;
using System.Linq;
using local_events_app.Data;
using local_events_app.Models;

namespace local_events_app.Services
{
    public class EventService
    {
        private readonly AppDbContext _dbContext;

        public EventService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ScrapedEvent> GetSavedEvents()
        {
            // Logic to retrieve saved events from the database
            return _dbContext.ScrapedEvents.ToList();
        }

        public void SaveEvent(ScrapedEvent eventToSave)
        {
            // Logic to save an event to the database
            _dbContext.ScrapedEvents.Add(eventToSave);
            _dbContext.SaveChanges();
        }

        public void DeleteEvent(string eventTitleToDelete)
        {
            // Logic to delete an event from the database
            var eventToDelete = _dbContext.ScrapedEvents.FirstOrDefault(e => e.Title == eventTitleToDelete);

            if (eventToDelete != null)
            {
                _dbContext.ScrapedEvents.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
