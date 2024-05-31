using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class Location
    {
        // ATTRIBUTES
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        // METHODS
        public Location() { }
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public (double, double) GetCoordinates()
        {
            return (Latitude, Longitude);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Location other = (Location)obj;
            return other.Latitude == this.Latitude && other.Longitude == this.Longitude;
        }
    }
}
