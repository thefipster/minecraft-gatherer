using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheFipster.Minecraft.Speedrun.Web.Services
{
    public class HelloWorldService : IHostedService, IDisposable
    {
        private readonly ILogger<HelloWorldService> _logger;
        private readonly Timer _timer;

        public HelloWorldService(ILogger<HelloWorldService> logger)
        {
            _logger = logger;
            _timer = new Timer(run, null, Timeout.Infinite, -1);
            _logger.LogInformation($"Hosted Service {GetType().Name} initialized.");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer.Change(5000, -1);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, -1);
            return Task.CompletedTask;
        }

        public void Dispose()
            => _timer.Dispose();

        private void run(object state)
        {
            _logger.LogInformation("Hello world service is saying: Hello world.");
            _timer.Change(5000, -1);
        }
    }
}
