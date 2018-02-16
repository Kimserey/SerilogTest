using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace LogServer
{
    public class Startup
    {
        public static Subject<IEnumerable<LogEventViewModel>> channel = new Subject<IEnumerable<LogEventViewModel>>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ServerSentEventMiddleware>();
            app.UseMvcWithDefaultRoute();
        }
    }
}
