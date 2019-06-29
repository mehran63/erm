using System;
using Microsoft.Extensions.DependencyInjection;

namespace Fluctuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start  processing...");

            var container = new DIContainer();
            var serviceProvider = container.BuildServiceProvider();
            var processor = serviceProvider.GetService<BatchFluctuationProcessor>();

            processor.Process();

            Console.WriteLine("Processing finished.");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
