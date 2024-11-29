using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uchebkaroflssss.Data
{
    internal class Event
    {
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public int EventScore { get; set; }
        public int CurrentEventUsers { get; set; }
        public DateTime EventCreatedDay { get; set; }
        public DateTime EventDateToEnd { get; set; }
        public string? EventDescription { get; set; }

    }
}
