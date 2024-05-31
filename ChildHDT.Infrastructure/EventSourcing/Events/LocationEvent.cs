using ChildHDT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.EventSourcing.Events
{
    public class LocationEvent : Event
    {
        public Location Location { get; set; }

        public LocationEvent(Location locationData, DateTime timestamp) : base(timestamp)
        {
            Location = locationData;
        }
    }
}
