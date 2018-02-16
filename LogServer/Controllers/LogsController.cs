using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace LogServer.Controllers
{
    [Route("api/logs")]
    public class LogsController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] LogEventsViewModel logs)
        {
            Startup.channel.OnNext(logs.Events);
            return Ok();
        }
    }
}
