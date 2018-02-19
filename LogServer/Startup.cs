using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
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
        public static ReplaySubject<LogEventViewModel> channel = new ReplaySubject<LogEventViewModel>(20);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(opt => {
                    opt.Authority = "http://localhost:5000/identity";
                    opt.ApiName = "log-api";
                    opt.RequireHttpsMetadata = false;
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseAuthentication();
            app.UseMiddleware<ServerSentEventMiddleware>();
            app.UseMvcWithDefaultRoute();
        }
    }
}
