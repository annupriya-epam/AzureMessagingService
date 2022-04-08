using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherDataIngestor
{
    public interface IMessageProcessor
    {
        void Process(string message);
    }

    public class MessageProcessor : IMessageProcessor
    {
        private readonly ITransientService transientService;
        private readonly ISingletonService singletonService;
        private readonly IScopedService scopedService;
        private readonly IOptions<MyConfigOptions> configOptions;

        public MessageProcessor(
            ITransientService transientService, 
            ISingletonService singletonService,
            IScopedService scopedService,
            IOptions<MyConfigOptions> configOptions)
        {
            this.transientService = transientService;
            this.singletonService = singletonService;
            this.scopedService = scopedService;
            this.configOptions = configOptions;
        }
        public void Process(string message)
        {
            if (message.Contains("exception")) throw new Exception("Exception found in message");

            transientService.Write("Message Processor");
            scopedService.Write("Message Processor");
            singletonService.Write("Message Processor");
        }
    }
}
