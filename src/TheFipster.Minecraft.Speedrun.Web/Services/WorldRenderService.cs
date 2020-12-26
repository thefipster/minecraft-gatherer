using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Threading;
using System.Threading.Tasks;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Services
{
    public class WorldRenderService : IHostedService, IDisposable
    {
        private const int WaitTimeInMs = 30000;
        private readonly ILogger<WorldRenderService> _logger;
        private readonly Container _container;
        private readonly IRenderQueue _queue;
        private readonly Timer _timer;

        public WorldRenderService(IRenderQueue queue, ILogger<WorldRenderService> logger, Container container)
        {
            _logger = logger;
            _container = container;
            _queue = queue;
            _timer = new Timer(tryExecute, null, Timeout.Infinite, -1);

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
        {
            _timer.Dispose();
        }

        private void tryExecute(object state)
        {
            try
            {
                if (canExecute())
                    using (AsyncScopedLifestyle.BeginScope(_container))
                        execute();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Render failed");
            }
            finally
            {
                _timer.Change(WaitTimeInMs, -1);
            }
        }

        private void execute()
        {
            var job = _queue.Dequeue();
            if (job == null)
                return;

            var renderer = _container.GetInstance<IMapRenderModule>();
            renderer.Render(job);
            _logger.LogInformation("Attempting map render.");
        }

        private bool canExecute()
        {
            return true;
        }
    }
}
