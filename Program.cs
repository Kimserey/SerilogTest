﻿using System;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
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
                .WriteTo.Console()
                .WriteTo.File(new CompactJsonFormatter(), "log.clef", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3)
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
