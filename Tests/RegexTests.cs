using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace local_events_app.test
{
    [TestClass]
    public class RegexTests
    {
        [TestMethod]
        public void TitleRegex_ValidTitle_ReturnsTrue()
        {
            // Arrange
            var validTitle = "Test Event";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(validTitle, "^[A-Za-z0-9 \\p{P}]+$");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DateRegex_ValidDate_ReturnsTrue()
        {
            // Arrange
            var validDate = "01-01-2023";

            // Act
            var result = System.Text.RegularExpressions.Regex.IsMatch(validDate, @"^\d{2}-\d{2}-\d{4}$");

            // Assert
            Assert.IsTrue(result);
        }
    }
}
