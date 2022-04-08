using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

[assembly: FunctionsStartup(typeof(WeatherDataIngestor.Startup))]

namespace WeatherDataIngestor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IMessageProcessor, MessageProcessor>();
            builder.Services.AddLogging();

            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddSingleton<ISingletonService, SingletonService>();
            builder.Services.AddTransient<IAnotherDependency, AnotherDependency>();

            builder.Services.AddOptions<MyConfigOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("MyConfig").Bind(settings);
                });
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);

            var context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(
                    Path.Combine(context.ApplicationRootPath, "appsettings.json"),
                    optional: true, reloadOnChange: false)
                .AddJsonFile(
                    Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"),
                    optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }
    }
}