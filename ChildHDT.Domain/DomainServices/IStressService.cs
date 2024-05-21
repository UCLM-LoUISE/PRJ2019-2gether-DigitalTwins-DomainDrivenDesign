using ChildHDT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Domain.DomainServices
{
    public interface IStressService
    {
        public abstract Stress CalculateStress(IFeatures features);
    }
}
