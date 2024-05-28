using ChildHDT.Domain.DomainServices;
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
    public class StressRegistry : EventStore<StressEvent>
    {
        public StressRegistry(Guid id) : base(id, "stress") { }

        protected override StressEvent DeserializeEvent(string payload)
        {
            var data = JsonSerializer.Deserialize<Stress>(payload);
            return new StressEvent(data, DateTime.Now);
        }
    }
}
