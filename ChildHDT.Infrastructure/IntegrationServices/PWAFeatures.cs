using ChildHDT.Domain.ValueObjects;
using ChildHDT.Infrastructure.EventSourcing.Registries;
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
        public PWAFeatures(Guid childId) 
        {
            SpeedRegistry = new SpeedRegistry(childId);
            LocationRegistry = new LocationRegistry(childId);
            OrientationRegistry = new OrientationRegistry(childId);
            StressRegistry = new StressRegistry(childId);
        }
    }
}
