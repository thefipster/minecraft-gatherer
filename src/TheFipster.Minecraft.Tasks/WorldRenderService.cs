using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheFipster.Minecraft.HostedServices
{
    public class WorldRenderService : IHostedService, IDisposable
    {
        private readonly ILogger<WorldRenderService> _logger;
        private readonly Timer _timer;

        public WorldRenderService(ILogger<WorldRenderService> logger)
        {
            _logger = logger;
            _timer = new Timer(run, null, Timeout.Infinite, -1);
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
        {
            _timer.Dispose();
        }

        private void run(object state)
        {
            throw new NotImplementedException();
        }
    }
}
