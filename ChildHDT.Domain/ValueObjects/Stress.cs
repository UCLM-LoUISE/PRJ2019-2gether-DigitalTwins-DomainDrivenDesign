using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.ValueObjects
{
    public class Stress
    {
        // ATTRIBUTES
        public readonly double value;
        public readonly string level;

        // MEHTHODS
        public Stress(double value, string level)
        {
            this.value = value;
            this.level = level;
        }

    }
}
