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
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public StressMonitoringService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var stressService = scope.ServiceProvider.GetRequiredService<IStressService>();
                var rc = scope.ServiceProvider.GetRequiredService<RepositoryChild>();

                var children = await rc.GetAll();
                foreach (var child in children)
                {
                    var stressResult = await stressService.CalculateStress(child);
                    (child.Features as PWAFeatures)?.StressRegistry.ReceiveEvent(new StressEvent(stressResult, DateTime.Now));
                }
            }
        }
    }
}
