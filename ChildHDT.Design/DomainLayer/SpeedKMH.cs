using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class SpeedKMH
    {
        // ATTRIBUTES
        protected readonly double value;

        // METHODS
        public SpeedKMH(double distanceInKmPerHour) 
        {
            value = distanceInKmPerHour;
        }

        public SpeedMS ToMetersPerSecond (double distanceInKmPerHour) {
            double result = (distanceInKmPerHour * 1000) / 3600;
            return new SpeedMS(result);
        }
    }
}
