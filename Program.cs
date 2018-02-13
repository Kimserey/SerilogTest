using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Murmur;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;

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
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("X", "Demo")
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{EventType:x8} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(new CompactJsonFormatter(), "log.clef", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3)
                .WriteTo.AzureTableStorage("UseDevelopmentStorage=true;")
                .CreateLogger();

            using (LogContext.PushProperty("hehe", "hoho"))
                logger.Information("Hello, world!");

            var itemNumber = 10;
            var itemCount = 999;
            logger.Information("Processing item {ItemNumber} of {ItemCount}", itemNumber, itemCount);

            Console.WriteLine("Hello World!");
        }
    }
}
