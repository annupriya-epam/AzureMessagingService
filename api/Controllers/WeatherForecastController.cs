using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace queue_storage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post([FromBody]WeatherForecast data)
        {
            string connectionstring = "";
            string queueName = "queue1";
            QueueClient _queueClient = new QueueClient(connectionstring, queueName);
            var message = JsonSerializer.Serialize(data);
            await _queueClient.SendMessageAsync(message);
            await _queueClient.SendMessageAsync(message, null, TimeSpan.FromSeconds(10));
            await _queueClient.SendMessageAsync(message, null, TimeSpan.FromSeconds(-1));

        }
    }
}
