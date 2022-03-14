using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WeatherForecast.Web.Api.HostedServices
{
    public class CheckWarmUpHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<CheckWarmUpHostedService> _logger;
        private Timer _timer = null!;

        public CheckWarmUpHostedService(ILogger<CheckWarmUpHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Is app warm? {@IsWarmedUp}", WarmUpStatus.IsWarmedUp);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}