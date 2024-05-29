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
        private bool isRunning;

        public StressMonitoringService(IStressService stressService, RepositoryChild rc)
        {
            this.stressService = stressService;
            this.rc = rc;
            isRunning = true;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            isRunning = true;
            await ExecuteAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            isRunning = false;
            await Task.CompletedTask;
        }

        private async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested && isRunning)
            {
                var children = await rc.GetAll();
                foreach (var child in children)
                {
                    var stressResult = await stressService.CalculateStress(child);
                    (child.Features as PWAFeatures)?.StressRegistry.ReceiveEvent(new StressEvent(stressResult, DateTime.Now));
                }

                await Task.Delay(4000, stoppingToken);
            }
        }
    }
}
