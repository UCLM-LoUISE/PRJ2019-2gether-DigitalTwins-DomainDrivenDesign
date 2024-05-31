using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Registries;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.IntegrationServices
{
    public class PWAFeatures: IFeatures
    {
        private readonly IConfiguration _configuration;
        public SpeedRegistry SpeedRegistry { get; set; }
        public LocationRegistry LocationRegistry { get; set; }
        public OrientationRegistry OrientationRegistry { get; set; }
        public StressRegistry StressRegistry { get; set; }

        public PWAFeatures(Guid childId, IConfiguration configuration) 
        {
            _configuration = configuration;
            SpeedRegistry = new SpeedRegistry(childId, _configuration);
            LocationRegistry = new LocationRegistry(childId, _configuration);
            OrientationRegistry = new OrientationRegistry(childId, _configuration);
            StressRegistry = new StressRegistry(childId, _configuration);
        }
    }
}
