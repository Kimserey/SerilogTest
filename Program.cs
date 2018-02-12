using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace SerilogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3)
                .CreateLogger();

            logger.Information("Hello, world!");
            
            Console.WriteLine("Hello World!");
        }
    }
}
