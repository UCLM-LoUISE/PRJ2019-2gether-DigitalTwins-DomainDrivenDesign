using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class Orientation
    {
        // ATTRIBUTES
        [JsonPropertyName("angle")]
        public double angle { get; set; }
        [JsonPropertyName("proximity")]
        public int proximity { get; set; }
        
        // METHODS
        public Orientation() { }
        public Orientation(double angleInDegrees) 
        {
            angle = angleInDegrees;
            proximity = CalculateProximity(angleInDegrees);
        }

        public int CalculateProximity(double angleInDegrees)
        {
            if (angleInDegrees < 0) { throw new ArgumentException(); }
            else if (angleInDegrees <= 5) { return 3; }
            else if (angleInDegrees <= 25) { return 2; }
            else if (angleInDegrees <= 60) { return 1; }
            else { return 0; }
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Orientation other = (Orientation)obj;
            return angle == other.angle && proximity == other.proximity;
        }
    }
}
