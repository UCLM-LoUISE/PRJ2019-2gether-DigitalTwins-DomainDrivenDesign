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
    public class LocationRegistry : EventStore<LocationEvent>
    {
        public LocationRegistry(Guid id) : base(id, "location") { }

        protected override LocationEvent DeserializeEvent(string payload)
        {
            var data = JsonSerializer.Deserialize<Location>(payload);
            return new LocationEvent(data, DateTime.Now);
        }
    }
}
