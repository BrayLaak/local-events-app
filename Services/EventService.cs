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

        public List<Event> GetEvents()
        {
            // Logic to retrieve saved events from the database
            return _dbContext.Events.ToList();
        }

        public void AddEvent(Event eventToAdd)
        {
            // Logic to save an event to the database
            _dbContext.Events.Add(eventToAdd);
            _dbContext.SaveChanges();
        }

        public void DeleteEvent(int eventIDToDelete)
        {
            // Logic to delete an event from the database
            var eventToDelete = _dbContext.Events.FirstOrDefault(e => e.ID == eventIDToDelete);

            if (eventToDelete != null)
            {
                _dbContext.Events.Remove(eventToDelete);
                _dbContext.SaveChanges();
            }
        }

        public Event GetEventById(int eventId)
        {
            return _dbContext.Events.FirstOrDefault(e => e.ID == eventId);
        }

        public void ModifyEvent(int eventIDtoModify, Event eventToModify)
        {
            // Logic to modify an event in the database
            var eventToModifyinDb = _dbContext.Events.FirstOrDefault(e => e.ID == eventIDtoModify);

            if (eventToModifyinDb != null)
            {
                eventToModifyinDb.Title = eventToModify.Title;
                eventToModifyinDb.Date = eventToModify.Date;
                eventToModifyinDb.Description = eventToModify.Description;
                eventToModifyinDb.Location = eventToModify.Location;
                _dbContext.SaveChanges();
            }
        }
    }
}
