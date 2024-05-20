using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.EventSourcing.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.EventSourcing.Registries
{
    public class OrientationRegistry : EventStore<OrientationEvent>
    {
        public OrientationRegistry(Child child) : base(child, "orientation") { }

        protected override OrientationEvent DeserializeEvent(string payload)
        {
            var data = JsonSerializer.Deserialize<double>(payload);
            return new OrientationEvent(data, DateTime.Now);
        }
    }
}
