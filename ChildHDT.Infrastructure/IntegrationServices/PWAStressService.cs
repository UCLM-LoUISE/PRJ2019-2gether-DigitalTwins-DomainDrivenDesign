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

        private readonly INotificationHandler notificationHandler;

        // METHODS

        public PWAStressService(INotificationHandler nh) 
        {
            notificationHandler = nh;
        }

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
                child.StressLevelShotUp(notificationHandler);
            } else
            {
                level = "Controlled";
            }
            return level;
        }
    }
}
