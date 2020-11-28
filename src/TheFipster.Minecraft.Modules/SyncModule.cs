using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Abstractions;
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
        private readonly IRunImportStore _importStore;
        private readonly IRunAnalyticsStore _analyticsStore;
        private readonly ILogger<SyncModule> _logger;

        public SyncModule(
            IWorldLoaderModule worldLoaderModule,
            IImportRunModule importModule,
            IEnhanceModule enhanceModule,
            IRunImportStore runImportStore,
            IAnalyticsModule analyticsModule,
            IRunAnalyticsStore runAnalyticsStore,
            ILogger<SyncModule> logger)
        {
            _loader = worldLoaderModule;
            _importer = importModule;
            _enhancer = enhanceModule;
            _analytics = analyticsModule;
            _importStore = runImportStore;
            _analyticsStore = runAnalyticsStore;
            _logger = logger;
        }

        public IEnumerable<RunImport> Synchronize(bool withForce = false)
        {
            var runs = new List<RunImport>();
            var analytics = new List<RunAnalytics>();
            var worlds = _loader.Load(withForce);
            foreach (var world in worlds)
            {
                var run = _importer.Import(world);
                run = _enhancer.Enhance(run);
                storeImport(run);
                runs.Add(run);

                var analytic = _analytics.Analyze(run);
                storeAnalytics(analytic);
                analytics.Add(analytic);
            }

            return runs;
        }

        private void storeImport(RunImport import)
        {
            if (_importStore.Exists(import.Worldname))
            {
                _importStore.Update(import);
                _logger.LogDebug($"Import store updated run {import.Worldname}.");
            }
            else
            {
                _importStore.Add(import);
                _logger.LogDebug($"Import store created run {import.Worldname}.");
            }
        }

        private void storeAnalytics(RunAnalytics analytics)
        {
            if (_analyticsStore.Exists(analytics.Worldname))
            {
                _analyticsStore.Update(analytics);
                _logger.LogDebug($"Import store updated run {analytics.Worldname}.");
            }
            else
            {
                _analyticsStore.Add(analytics);
                _logger.LogDebug($"Import store created run {analytics.Worldname}.");
            }
        }
    }
}
