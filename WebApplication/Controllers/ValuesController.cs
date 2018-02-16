using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var values = new string[] { "value1", "value2" };
            _logger.LogInformation("Test {values}", values);
            return Ok();
        }

        [HttpGet("errors")]
        public IActionResult Errors()
        {
            _logger.LogError("Some Errors!!!!");
            _logger.LogCritical("Some Critical!!!!");
            _logger.LogWarning("Some Warning!!!!");
            _logger.LogInformation("Some Information!!!!");
            return Ok();
        }

        [HttpGet("exceptions")]
        public IActionResult Exceptions()
        {
            throw new ArgumentException("Some argument exception");
        }
    }
}
