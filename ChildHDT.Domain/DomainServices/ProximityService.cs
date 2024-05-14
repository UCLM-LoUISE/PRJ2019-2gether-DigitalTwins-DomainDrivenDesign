using ChildHDT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.DomainServices
{
    public class ProximityService
    {
        // ATTRIBUTES
        const double earthRadiusKm = 6371;
            
        // METHODS
        public double CalculateProximity(Location first, Location second)
        {

            var dLat = DegreeToRadian(first.GetLatitude() - second.GetLatitude());
            var dLon = DegreeToRadian(first.GetLatitude() - second.GetLatitude());

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreeToRadian(first.GetLatitude())) * Math.Cos(DegreeToRadian(second.GetLatitude())) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = earthRadiusKm * c;
            return distance;
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
