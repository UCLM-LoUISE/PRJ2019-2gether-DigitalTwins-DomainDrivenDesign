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
        public SpeedRegistry(Guid id, string server, int port, string user, string pwd) : base(id, "speed", server, port, user, pwd) { }

        public override SpeedEvent GetLastEvent()
        {
            if (Events.Count == 0) return new SpeedEvent(new SpeedMS(0), DateTime.Now);
            return Events[Events.Count - 1];
        }

        protected override SpeedEvent DeserializeEvent(string payload)
        {

            var data = JsonSerializer.Deserialize<SpeedMS>(payload);
            return new SpeedEvent(data, DateTime.Now);
        }
    }
}
