using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using local_events_app.Models;

namespace local_events_app.Services
{
    public class LexingtonGovScraperService
    {
        private readonly HttpClient httpClient;

        public LexingtonGovScraperService()
        {
            // Initialize HttpClient
            httpClient = new HttpClient();
        }

        public async Task<List<ScrapedEvent>> ScrapeEventsAsync()
        {
            try
            {
                // URL of the website
                string url = "https://www.lexingtonky.gov/calendar/events";

                // Fetch HTML content
                string htmlContent = await httpClient.GetStringAsync(url);

                // Parse HTML using HtmlAgilityPack
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                // Extract event details
                var events = ExtractEvents(htmlDocument);

                // Use the 'events' list containing extracted event details as needed
                foreach (var scrapedEvent in events)
                {
                    Console.WriteLine($"Title: {scrapedEvent.Title}, Date: {scrapedEvent.Date}");
                    Console.WriteLine($"Description: {scrapedEvent.Description}");
                    Console.WriteLine($"Location: {scrapedEvent.Location}");
                    Console.WriteLine($"URL: {scrapedEvent.Url}");
                    Console.WriteLine();

                    // Add additional details as needed
                }

                return events;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<ScrapedEvent>();
            }
        }

        public List<ScrapedEvent> ExtractEvents(HtmlDocument document)
        {
            List<ScrapedEvent> events = new List<ScrapedEvent>();

            // Adjust these XPaths based on the actual HTML structure of the website
            var eventNodes = document.DocumentNode.SelectNodes("//div[@class='list-event-container']");

            if (eventNodes != null)
            {
                foreach (var node in eventNodes)
                {
                    var title = node.SelectSingleNode(".//span[@class='list-event-title']")?.InnerText.Trim();
                    var date = node.SelectSingleNode(".//span[@class='list-event-date']")?.InnerText.Trim();
                    var description = node.SelectSingleNode(".//div[@class='list-text-container']/p")?.InnerText.Trim();
                    var location = node.SelectSingleNode(".//div[@class='list-text-container']/span[contains(text(), 'Location')]/following-sibling::span")?.InnerText.Trim();
                    var url = node.SelectSingleNode(".//a[@href]")?.GetAttributeValue("href", "");

                    // Create a ScrapedEvent object and add it to the list
                    events.Add(new ScrapedEvent
                    {
                        Title = title,
                        Date = date,
                        Description = description,
                        Location = location,
                        Url = url
                        // Add other details as needed
                    });
                }
            }

            return events;
        }
    }
}
