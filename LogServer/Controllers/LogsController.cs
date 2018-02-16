using Microsoft.AspNetCore.Mvc;

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
