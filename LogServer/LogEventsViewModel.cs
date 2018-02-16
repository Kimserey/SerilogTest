using System;
using System.Collections.Generic;

namespace LogServer
{
    public class LogEventViewModel
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string RenderedMessage { get; set; }
        public string MessageTemplate { get; set; }
        public Dictionary<string, object> Properties { get; set; }
    }

    public class LogEventsViewModel
    {
        public IEnumerable<LogEventViewModel> Events { get; set; }
    }
}
