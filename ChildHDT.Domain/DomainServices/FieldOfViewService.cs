using ChildHDT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.DomainServices
{
    public class FieldOfViewService
    {
        // METHODS

        public static Orientation CalculateFieldOfViewService(Location one, Location other, double azimuth)
        {

            double x1 = one.Latitude, y1 = one.Longitude;
            double x2 = other.Latitude, y2 = other.Longitude;

            double azimuth1Rad = DegreesToRadians(azimuth + 90.0); 
            double[] orientationVector = { Math.Cos(azimuth1Rad), Math.Sin(azimuth1Rad) };

            double[] vectorToSecondPerson = { x2 - x1, y2 - y1 };

            double magnitude = Math.Sqrt(vectorToSecondPerson[0] * vectorToSecondPerson[0] + vectorToSecondPerson[1] * vectorToSecondPerson[1]);
            double[] normalizedVectorToSecondPerson = { vectorToSecondPerson[0] / magnitude, vectorToSecondPerson[1] / magnitude };

            double dotProduct = orientationVector[0] * normalizedVectorToSecondPerson[0] + orientationVector[1] * normalizedVectorToSecondPerson[1];

            double angle = Math.Acos(dotProduct);

            double angleInDegrees = angle * 180.0 / Math.PI;

            return new Orientation(angleInDegrees);

        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

    }
}
