using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LogServer
{
    public class ServerSentEventMiddleware
    {
        private readonly RequestDelegate _next;

        public ServerSentEventMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().Equals("/sse"))
            {
                var response = context.Response;
                response.Headers.Add("Content-Type", "text/event-stream");

                var sub = Startup.channel.Subscribe(async e =>
                {
                    await response.WriteAsync($"data:{JsonConvert.SerializeObject(e)}\n\n");
                });

                context.RequestAborted.WaitHandle.WaitOne();

                sub.Dispose();
            }
            else
            {
                await _next(context);
            }
        }
    }
}
