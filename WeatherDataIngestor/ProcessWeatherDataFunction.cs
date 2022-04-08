using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace WeatherDataIngestor
{
    public class ProcessWeatherDataFunction
    {
        private readonly IMessageProcessor messageProcessor;
        private readonly IAnotherDependency anotherDependency;
        private readonly ILogger<ProcessWeatherDataFunction> log;

        public ProcessWeatherDataFunction(
            IMessageProcessor messageProcessor, IAnotherDependency anotherDependency, ILogger<ProcessWeatherDataFunction> log)
        {
            this.messageProcessor = messageProcessor;
            this.anotherDependency = anotherDependency;
            this.log = log;
        }

        [FunctionName("ProcessWeatherData")]
        public void Run([QueueTrigger("add-weatherdata", Connection = "WeatherDataQueue")]string myQueueItem)
        {
            messageProcessor.Process(myQueueItem);
            anotherDependency.Process(myQueueItem);

            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
