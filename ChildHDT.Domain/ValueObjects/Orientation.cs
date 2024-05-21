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
            CalculateProximity(angleInDegrees);
        }

        public void CalculateProximity(double angleInDegrees)
        {
            if (angleInDegrees < 0) { throw new ArgumentException(); }
            else if (angleInDegrees <= 5) { this.proximity = 3; }
            else if (angleInDegrees <= 25) { this.proximity = 2; }
            else if (angleInDegrees <= 60) { this.proximity = 1; }
            else { this.proximity = 0; }
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
