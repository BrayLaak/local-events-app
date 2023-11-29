/*
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using local_events_app.Data;
using local_events_app.Models;

namespace local_events_app.Services
{

    public class MeetupService
    {
        private readonly GraphQLHttpClient _graphQLClient;
        private readonly MeetupDbContext _dbContext;

        public MeetupService(string baseUrl, string apiKey, MeetupDbContext dbContext)
        {
            var options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(baseUrl),
            };

            var graphQLClient = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            _graphQLClient = graphQLClient;
            _dbContext = dbContext;
        }

        public async Task<List<MeetupEvent>> SearchLocalEventsAsync(string location)
        {
            var query = @"
            query ($lat: Float, $lon: Float, $startDate: String, $endDate: String) {
                eventsSearch(
                    input: {
                        location: {lat: $lat, lon: $lon}
                        dateRange: {start: $startDate, end: $endDate}
                    }
                ) {
                    events {
                        id
                        name
                        description
                        local_date
                        link
                    }
                }
            }";

            var variables = new
            {
                lat = 38.0406,
                lon = -84.5037,
                startDate = DateTime.Now.ToString("yyyy-MM-dd"),
                endDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd")
            };

            var request = new GraphQLRequest
            {
                Query = query,
                Variables = variables
            };

            try
            {
                var response = await _graphQLClient.SendQueryAsync<MeetupEventsResponse>(request);

                // Print or log the raw GraphQL response
                Console.WriteLine($"Raw GraphQL Response: {response}");

                if (response.Errors != null && response.Errors.Any())
                {
                    foreach (var error in response.Errors)
                    {
                        Console.WriteLine($"GraphQL Error: {error.Message}");
                    }

                    return new List<MeetupEvent>();
                }

                var meetupEvents = response.Data?.eventsSearch?.events?.Select(e => new MeetupEvent
                {
                    Id = e.id,
                    Name = e.name,
                    Description = e.description,
                    Time = DateTime.Parse(e.local_date),
                    Url = e.link
                }).ToList();

                return meetupEvents ?? new List<MeetupEvent>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error querying Meetup API: {ex.Message}");
                return new List<MeetupEvent>();
            }
        }
    }
}
*/