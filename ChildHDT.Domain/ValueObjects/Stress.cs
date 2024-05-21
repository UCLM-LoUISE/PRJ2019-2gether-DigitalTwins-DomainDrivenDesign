using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class Stress
    {
        // ATTRIBUTES
        [JsonPropertyName("value")]
        public double value { get; set; }
        [JsonPropertyName("level")]
        public string level { get; set; }

        // MEHTHODS
        public Stress(double value, string level)
        {
            this.value = value;
            this.level = level;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Stress other = (Stress)obj;
            return value == other.value && level == other.level;
        }

    }
}
