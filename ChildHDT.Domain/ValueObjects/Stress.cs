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
        protected readonly double value;
        protected readonly double level;

        // MEHTHODS
        public Stress(double value, double level)
        {
            this.value = value;
            this.level = level;
        }

    }
}
