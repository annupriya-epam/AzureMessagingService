using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherDataIngestor
{
    public interface IAnotherDependency
    {
        void Process(string message);
    }

    public class AnotherDependency : IAnotherDependency
    {
        private readonly ITransientService transientService;
        private readonly ISingletonService singletonService;
        private readonly IScopedService scopedService;

        public AnotherDependency(
          ITransientService transientService,
          ISingletonService singletonService,
          IScopedService scopedService)
        {
            this.transientService = transientService;
            this.singletonService = singletonService;
            this.scopedService = scopedService;
        }

        public void Process(string message)
        {
            transientService.Write("Another Dependency");
            scopedService.Write("Another Dependency");
            singletonService.Write("Another Dependency");
        }
    }
}
