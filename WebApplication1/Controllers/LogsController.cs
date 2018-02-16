using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class LogEventViewModel
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string RenderedMessage { get; set; }
    }

    public class LogEventsViewModel
    {
        public IEnumerable<LogEventViewModel> Events { get; set; }
    }

    [Route("api/logs")]
    public class LogsController : Controller
    {
        // Receives logs from Serilog HTTP Sink.
        [HttpPost]
        public IActionResult Post([FromBody] LogEventsViewModel logs)
        {
            Startup.channel.OnNext("");
            return Ok();
        }
    }
}
