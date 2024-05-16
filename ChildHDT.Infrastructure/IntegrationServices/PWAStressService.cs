using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.ValueObjects;
using ChildHDT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChildHDT.Infrastructure.IntegrationServices
{
    public class PWAStressService : IStressService
    {
        // METHODS
        public Stress CalculateStress(Child child)
        {
            var value = 0; // Calcular con lo de Luis
            var level = EvaluateStress(child, value);
            return new Stress(value, level);
        }

        public string EvaluateStress(Child child, double value) 
        {
            string level;
            if (value > 0.6)
            {
                level = "High";
                child.StressLevelShotUp();
            } else
            {
                level = "Controlled";
            }
            return level;
        }
    }
}
