using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
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

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Path.ToString().Equals("/sse"))
                {
                    var response = ctx.Response;
                    response.Headers.Add("Content-Type", "text/event-stream");

                    for (var i = 0; true; ++i)
                    {
                        await response.WriteAsync($"data: Middleware {i} at {DateTime.Now}\r\r");

                        response.Body.Flush();
                        await Task.Delay(5 * 1000);
                    }
                }

                await next.Invoke();
            });

            app.UseMvc();
        }
    }
}
