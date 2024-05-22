using ChildHDT.Domain.Entities;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Events;
using Microsoft.Extensions.Configuration;
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
        public OrientationRegistry(Child child, IConfiguration configuration) : base(child, "location", configuration) { }

        protected override OrientationEvent DeserializeEvent(string payload)
        {
            var data = JsonSerializer.Deserialize<Orientation>(payload);
            return new OrientationEvent(data, DateTime.Now);
        }
    }
}
