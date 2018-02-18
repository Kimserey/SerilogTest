using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogServer.Controllers
{
    [Authorize]
    [Route("api/logs")]
    public class LogsController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] LogEventsViewModel logs)
        {
            foreach(var e in logs.Events)
                Startup.channel.OnNext(e);

            return Ok();
        }
    }
}
