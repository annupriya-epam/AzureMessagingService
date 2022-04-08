using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherDataIngestor
{
    public interface ITransientService
    {
        void Write(string message);
    }

    public interface IScopedService
    {
        void Write(string message);
    }

    public interface ISingletonService
    {
        void Write(string message);
    }

    public class TransientService : ITransientService
    {
        private readonly ILogger<TransientService> logger;

        public TransientService(ILogger<TransientService> logger)
        {
            Random = Guid.NewGuid().ToString();
            this.logger = logger;
        }

        public string Random { get; }

        public void Write(string message)
        {
            logger.LogInformation("Transient - {message}, {Random}", message, Random);
        }
    }

    public class SingletonService : ISingletonService
    {
        private readonly ILogger<TransientService> logger;

        public SingletonService(ILogger<TransientService> logger)
        {
            Random = Guid.NewGuid().ToString();
            this.logger = logger;
        }

        public string Random { get; }

        public void Write(string message)
        {
            logger.LogInformation("Singleton - {message}, {Random}", message, Random);
        }
    }

    public class ScopedService : IScopedService
    {
        private readonly ILogger<TransientService> logger;

        public ScopedService(ILogger<TransientService> logger)
        {
            Random = Guid.NewGuid().ToString();
            this.logger = logger;
        }

        public string Random { get; }

        public void Write(string message)
        {
            logger.LogInformation("Scoped - {message}, {Random}", message, Random);
        }
    }
}
