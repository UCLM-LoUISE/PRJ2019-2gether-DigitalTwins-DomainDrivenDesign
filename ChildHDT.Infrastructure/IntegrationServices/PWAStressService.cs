using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.IntegrationServices
{
    public class PWAStressService : IStressService
    {
        // METHODS
        public Stress CalculateStress(PWAFeatures features)
        {
            return new Stress(0, null);
        }
    }
}
