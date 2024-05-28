using ChildHDT.Domain.DomainServices;
using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.EventSourcing.Events;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.IntegrationServices;

namespace ChildHDT.API.ApplicationServices
{
    public class StressMonitoringService
    {
        private readonly PWAStressService stressService;
        private readonly RepositoryChild rc;
        private bool isRunning;

        public StressMonitoringService(PWAStressService stressService, RepositoryChild rc)
        {
            this.stressService = stressService;
            this.rc = rc;
            isRunning = true;

            var stressThread = new Thread(StressLoop);
            stressThread.Start();
        }

        private void StressLoop()
        {
            while (isRunning)
            {
                var children = rc.GetAll().Result;
                foreach (var child in children)
                {
                    var stressResult = stressService.CalculateStress(child).Result;
                    (child.Features as PWAFeatures).StressRegistry.ReceiveEvent(new StressEvent(stressResult, DateTime.Now));
                }

                Thread.Sleep(4000);
            }
        }

        public void Stop()
        {
            isRunning = false;
        }
    }


}

