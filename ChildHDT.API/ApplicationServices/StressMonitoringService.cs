using System;
using System.Threading;
using System.Threading.Tasks;
using ChildHDT.Infrastructure.IntegrationServices;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Domain.Entities;
using ChildHDT.Infrastructure.EventSourcing.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ChildHDT.Domain.DomainServices;

namespace ChildHDT.API.ApplicationServices
{
    public class StressMonitoringService : IHostedService
    {
        private readonly IStressService stressService;
        private readonly RepositoryChild rc;
        private Timer _timer;

        public StressMonitoringService(IStressService stressService, RepositoryChild rc)
        {
            this.stressService = stressService;
            this.rc = rc;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var children = rc.GetAll().Result;
            foreach (var child in children)
            {
                var stressResult = stressService.CalculateStress(child).Result;
                (child.Features as PWAFeatures)?.StressRegistry.ReceiveEvent(new StressEvent(stressResult, DateTime.Now));
            }
        }
        
    }
}
