using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class SpeedMS
    {
        // ATTRIBUTES
        public double value;

        // METHODS
        public SpeedMS(double distanceInMetersPerSecond) {
            value = distanceInMetersPerSecond;
        }

        public SpeedKMH ToKmPerHour(double distanceInMetersPerSecond)
        {
            double result = (distanceInMetersPerSecond * 3600) / 1000;
            return new SpeedKMH(result);
        }


    }
}
