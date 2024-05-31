using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.EventSourcing.Events
{
    public abstract class Event
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        protected Event(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
    }
}
