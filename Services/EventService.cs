using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging;
using local_events_app.Data;
using local_events_app.Models;

namespace local_events_app.Services
{
    public class EventService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<EventService> _logger;

        public EventService(AppDbContext dbContext, ILogger<EventService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Event> GetEvents()
        {
            try
            {
                // Logic to retrieve saved events from the database
                return _dbContext.Events.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving events: {ex.Message}");
                return new List<Event>();
            }
        }

        public void AddEvent(Event eventToAdd)
        {
            try
            {
                // Validate the event object
                var validationResults = new List<ValidationResult>();
                if (Validator.TryValidateObject(eventToAdd, new ValidationContext(eventToAdd), validationResults, true))
                {
                    // Logic to save an event to the database
                    _dbContext.Events.Add(eventToAdd);
                    _dbContext.SaveChanges();
                    Console.WriteLine("Event added successfully!");
                }
                else
                {
                    // Log validation errors
                    foreach (var validationResult in validationResults)
                    {
                        _logger.LogError($"Validation Error: {validationResult.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding event: {ex.Message}");
            }
        }

        public void DeleteEvent(int eventIDToDelete)
        {
            try
            {
                // Logic to delete an event from the database
                var eventToDelete = _dbContext.Events.FirstOrDefault(e => e.ID == eventIDToDelete);

                if (eventToDelete != null)
                {
                    _dbContext.Events.Remove(eventToDelete);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting event: {ex.Message}");
            }
        }

        public Event GetEventById(int eventId)
        {
            try
            {
                return _dbContext.Events.FirstOrDefault(e => e.ID == eventId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving event by ID: {ex.Message}");
                return null;
            }
        }

        public void ModifyEvent(int eventIDtoModify, Event eventToModify)
        {
            try
            {
                // Validate the event object
                var validationResults = new List<ValidationResult>();
                if (Validator.TryValidateObject(eventToModify, new ValidationContext(eventToModify), validationResults, true))
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
                        Console.WriteLine("Event modified successfully!");
                    }
                }
                else
                {
                    // Log validation errors
                    foreach (var validationResult in validationResults)
                    {
                        _logger.LogError($"Validation Error: {validationResult.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error modifying event: {ex.Message}");
            }
        }
    }
}
