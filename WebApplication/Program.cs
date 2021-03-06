﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Shared;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((ctx, cfg) =>
                {
                    cfg.ReadFrom.Configuration(ctx.Configuration)
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithProperty("Application", ctx.Configuration["Application"])
                        .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
                        .WriteTo.RollingFile("Logs\\Web-{Date}.log", outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Application} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
                        .WriteTo.DurableHttp(
                            requestUri: "http://localhost:5500/api/logs",
                            bufferPathFormat: "Logs\\Buffer\\Buffer-{Date}.json");
                            //httpClient: new OidcHttpClient(
                            //    ctx.Configuration["Identity:Client"],
                            //    ctx.Configuration["Secrets:WebLog"],
                            //    ctx.Configuration["Identity:Authority"]));
                })
                .Build();
    }
}
