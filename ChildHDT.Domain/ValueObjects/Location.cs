using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class Location
    {
        // ATTRIBUTES
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        // METHODS
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
