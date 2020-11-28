using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Modules
{
    public class SyncModule : ISyncModule
    {
        private readonly IWorldLoaderModule _loader;
        private readonly IImportRunModule _importer;
        private readonly IEnhanceModule _enhancer;
        private readonly IAnalyticsModule _analytics;
        private readonly IRunImportStore _runStore;
        private readonly ILogger<SyncModule> _logger;

        public SyncModule(
            IWorldLoaderModule worldLoaderModule,
            IImportRunModule importRunModule,
            IEnhanceModule enhanceModule,
            IRunImportStore runImportStore,
            IAnalyticsModule analyticsModule,
            ILogger<SyncModule> logger)
        {
            _loader = worldLoaderModule;
            _importer = importRunModule;
            _enhancer = enhanceModule;
            _analytics = analyticsModule;
            _runStore = runImportStore;
            _logger = logger;
        }

        public IEnumerable<RunImport> Synchronize(bool withForce = false)
        {
            var runs = new List<RunImport>();
            var analytics = new List<RunAnalytics>();
            var worlds = _loader.Load(withForce);
            var outcomeHistogram = new Dictionary<Outcomes, int>();
            foreach (var world in worlds)
            {
                var run = _importer.Import(world);
                run = _enhancer.Enhance(run);
                storeEnhancedImport(run);
                runs.Add(run);

                var analytic = _analytics.Analyze(run);
                analytics.Add(analytic);

                if (outcomeHistogram.ContainsKey(analytic.Outcome))
                    outcomeHistogram[analytic.Outcome]++;
                else
                    outcomeHistogram.Add(analytic.Outcome, 1);
            }

            return runs;
        }

        private void storeEnhancedImport(RunImport run)
        {
            if (_runStore.Exists(run.World.Name))
            {
                _runStore.Update(run);
                _logger.LogDebug($"Import store updated run {run.Worldname}.");
            }
            else
            {
                _runStore.Add(run);
                _logger.LogDebug($"Import store created run {run.Worldname}.");
            }
        }
    }
}
