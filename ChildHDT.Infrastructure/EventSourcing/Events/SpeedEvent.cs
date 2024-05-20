using ChildHDT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.EventSourcing.Events
{
    public class SpeedEvent : Event
    {
        [JsonPropertyName("speed")]
        public SpeedMS Speed { get; set; }

        public SpeedEvent(SpeedMS speed, DateTime timestamp) : base(timestamp)
        {
            Speed = speed;
        }
    }
}
