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
        protected readonly double Latitude;
        protected readonly double Longitude; 

        // METHODS
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public (double, double) GetCoordinates()
        {
            return (Latitude, Longitude);
        }
    }
}
