using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace LogServer.Controllers
{
    [Route("api/logs")]
    public class LogsController : Controller
    {
        // Receives logs from Serilog HTTP Sink.
        [HttpPost]
        public IActionResult Post([FromBody] object logs)
        {
            //foreach(var log in logs.Events)
            //{
            //    Startup.channel.OnNext(log.RenderedMessage);
            //}
            return Ok();
        }
    }
}
