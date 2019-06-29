using Fluctuation.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;

namespace Fluctuation
{
    public class DIContainer
    {
        public ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddTransient<IFileRecordLoader, CsvFileRecordLoader>();
            services.AddTransient<IMedianCalculator, BasicMedianCalculator>();
            services.AddTransient<IFluctuationProcessor, FluctuationProcessor>();
            services.AddTransient<IFluctuationReporter, ConsoleFluctuationReporter>();
            services.AddTransient<IFileSystem, FileSystem>();
            services.AddTransient<BatchFluctuationProcessor>();

            services.AddLogging(configure => configure.AddConsole());

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            services.Configure<FluctuationProcessingSettings>(options =>
                config.GetSection("FluctuationProcessingSettings").Bind(options));

            return services.BuildServiceProvider();
        }
    }
}
