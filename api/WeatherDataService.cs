using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace queue_storage
{
    public class WeatherDataService : BackgroundService
    {
        private readonly ILogger<WeatherDataService> _logger;
      

        public WeatherDataService(ILogger<WeatherDataService> logger)
        {
            _logger = logger;
          
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Reading from queue");
                string connectionstring = "";
                string queueName = "queue1";

                QueueClient _queueClient = new QueueClient(connectionstring, queueName);
                var queueMessage = await _queueClient.ReceiveMessageAsync();

                if (queueMessage.Value != null)
                {
                    //JsonSerializer.Deserialize<WeatherForecast>
                    var weatherData = (queueMessage.Value.MessageText);
                    _logger.LogInformation("New Mesasge Read: {weatherData}", weatherData);
                    // APplication process

                   await _queueClient.DeleteMessageAsync(queueMessage.Value.MessageId, queueMessage.Value.PopReceipt);
                }

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}