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
        [JsonPropertyName("score")]
        public int score { get; set; }
        
        // METHODS
        public Orientation() { }
        public Orientation(double angleInDegrees) 
        {
            angle = angleInDegrees;
            score = CalculateScore(angleInDegrees);
        }

        public int CalculateScore(double angleInDegrees)
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
            return angle == other.angle && score == other.score;
        }
    }
}
