using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Registries;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.IntegrationServices
{
    public class PWAFeatures: IFeatures
    {
        public SpeedRegistry SpeedRegistry { get; set; }
        public LocationRegistry LocationRegistry { get; set; }
        public OrientationRegistry OrientationRegistry { get; set; }
        public StressRegistry StressRegistry { get; set; }

        public PWAFeatures(Guid childId, string server, int port, string user, string pwd) 
        {
            SpeedRegistry = new SpeedRegistry(childId, server, port, user, pwd);
            LocationRegistry = new LocationRegistry(childId, server, port, user, pwd);
            OrientationRegistry = new OrientationRegistry(childId, server, port, user, pwd);
            StressRegistry = new StressRegistry(childId, server, port, user, pwd);
        }
    }
}
