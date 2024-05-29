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
            var server = _configuration["MQTT:Server"];
            var port = Convert.ToInt32(_configuration["MQTT:Port"]);
            var user = _configuration["MQTT:UserName"];
            var pwd = _configuration["MQTT:Password"];
            SpeedRegistry = new SpeedRegistry(childId, server, port, user, pwd);
            LocationRegistry = new LocationRegistry(childId, server, port, user, pwd);
            OrientationRegistry = new OrientationRegistry(childId, server, port, user, pwd);
            StressRegistry = new StressRegistry(childId, server, port, user, pwd);
        }
    }
}
