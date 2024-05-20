using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.EventSourcing.Events
{
    public class OrientationEvent : Event
    {
        public double Orientation { get; set; }

        public OrientationEvent(double orientationData, DateTime timestamp) : base(timestamp)
        {
            Orientation = orientationData;
        }
    }
}
