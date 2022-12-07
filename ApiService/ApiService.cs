using System;
using System.Collections;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace OutBox.ApiService
{
    public class ApiService
    {
        private readonly ILogger _logger;

        public ApiService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ApiService>();
        }

        [Function("ApiService")]
        public void Run([CosmosDBTrigger(
            databaseName: "outBoxEvents",
            collectionName: "eventHandler",
            ConnectionStringSetting = "",
            LeaseCollectionName = "leases")] IReadOnlyList<EventHandler> input)
        {
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("First document Id: " + input[0].Id);
            }
        }
    }

    public class EventHandler
    {
        public string Id { get; set; }

        public IDictionary<long, long> EventFlags { get; set; }

        public IDictionary<string, string> EventDetails { get; set; }

        public IDictionary <string, string> EventStatus { get; set; }
    }
}
