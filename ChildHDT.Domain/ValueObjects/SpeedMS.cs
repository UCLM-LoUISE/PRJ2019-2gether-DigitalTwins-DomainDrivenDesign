using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class SpeedMS
    {
        // ATTRIBUTES
        [JsonPropertyName("value")]
        public double value { get; set; }

        // METHODS
        public SpeedMS()
        {
        }

        public SpeedMS(double distanceInMetersPerSecond) {
            value = distanceInMetersPerSecond;
        }

        public SpeedKMH ToKmPerHour(double distanceInMetersPerSecond)
        {
            double result = (distanceInMetersPerSecond * 3600) / 1000;
            return new SpeedKMH(result);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            SpeedMS other = (SpeedMS)obj;
            return value == other.value;
        }
    }
}
