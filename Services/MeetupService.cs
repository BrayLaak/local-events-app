using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using local_events_app.Models;

namespace local_events_app.Services
{
    public class MeetupService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public MeetupService(string baseUrl, string apiKey)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
            _apiKey = apiKey;
        }

        public async Task<List<MeetupEvent>> SearchLocalEventsAsync(string location)
        {
            var apiUrl = $"{_baseUrl}/find/events?key={_apiKey}&sign=true&photo-host=public&page=20&location={location}";

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                var meetupEvents = JsonConvert.DeserializeObject<List<MeetupEvent>>(response);
                return meetupEvents;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exception (e.g., network issues)
                Console.WriteLine($"Error: {ex.Message}");
                return new List<MeetupEvent>();
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization exception
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<MeetupEvent>();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return new List<MeetupEvent>();
            }
        }
    }
}
