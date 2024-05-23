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
    public class SpeedRegistry : EventStore<SpeedEvent>
    {
        public SpeedRegistry(Guid id) : base(id, "speed") { }

        protected override SpeedEvent DeserializeEvent(string payload)
        {

            var data = JsonSerializer.Deserialize<SpeedMS>(payload);
            return new SpeedEvent(data, DateTime.Now);
        }
    }
}
