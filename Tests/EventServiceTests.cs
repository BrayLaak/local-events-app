using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using local_events_app.Data;
using local_events_app.Models;
using local_events_app.Services;
using NSubstitute;
using System.Collections.Generic;

namespace local_events_app.test
{
    [TestClass]
    public class EventServiceTests
    {
        [TestMethod]
        public void GetEvents_ReturnsEventsList()
        {
            // Arrange
            var dbContextSubstitute = Substitute.For<AppDbContext>();
            var loggerSubstitute = Substitute.For<ILogger<EventService>>();
            var eventService = new EventService(dbContextSubstitute, loggerSubstitute);

            // Act
            var result = eventService.GetEvents();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Event>));
        }
    }
}
