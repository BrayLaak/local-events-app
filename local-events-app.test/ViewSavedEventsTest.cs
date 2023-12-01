﻿using System;
using local_events_app.Models;
using local_events_app.Services;
using local_events_app.UI;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;

namespace local_events_app.Tests
{
    [TestClass]
    public class ViewSavedEventsTests
    {
        [TestMethod]
        public void ViewSavedEvents_ShouldDisplayEvents()
        {
            // Arrange
            var eventService = new EventService(MockAppDbContext());
            var consoleUI = new ConsoleUI(eventService, MockLogger<ConsoleUI>());

            // Act
            consoleUI.ViewSavedEvents();

            // Assert
            // Add assertions based on the expected behavior when viewing saved events
        }

        private AppDbContext MockAppDbContext()
        {
            // Implement a mock for AppDbContext if needed
            // You can use an in-memory database or a mocking library
            throw new NotImplementedException();
        }

        private ILogger<T> MockLogger<T>()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger()
                .ForContext<T>();
        }
    }
}
