using ChildHDT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.EventSourcing.Events
{
    public class StressEvent : Event
    {
        public Stress Stress { get; set; }

        public StressEvent(Stress stressData, DateTime timestamp) : base(timestamp)
        {
            Stress = stressData;
        }
    }
}
