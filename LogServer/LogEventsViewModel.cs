using System;
using System.Collections.Generic;

namespace LogServer
{
    public class LogEventsViewModel
    {
        public class LogEventViewModel
        {
            public DateTime Timestamp { get; set; }
            public string Message { get; set; }
            public string RenderedMessage { get; set; }
            public string MessageTemplate { get; set; }
            public Dictionary<string, string> Properties { get; set; }
        }

        public IEnumerable<LogEventViewModel> Events { get; set; }
    }
}
