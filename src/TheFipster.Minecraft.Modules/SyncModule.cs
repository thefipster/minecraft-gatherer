using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Modules.Models;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Modules
{
    public class SyncModule : ISyncModule
    {
        private readonly IWorldLoaderModule _loader;
        private readonly IImportModule _importer;
        private readonly IEnhanceModule _enhancer;
        private readonly IImportStore _importStore;
        private readonly IAnalyticsModule _analytics;
        private readonly IAnalyticsStore _analyticsStore;

        public SyncModule(
            IWorldLoaderModule worldLoaderModule,
            IImportModule importModule,
            IEnhanceModule enhanceModule,
            IImportStore runImportStore,
            IAnalyticsModule analyticsModule,
            IAnalyticsStore runAnalyticsStore)
        {
            _loader = worldLoaderModule;
            _importer = importModule;
            _enhancer = enhanceModule;
            _analytics = analyticsModule;
            _importStore = runImportStore;
            _analyticsStore = runAnalyticsStore;
        }

        public IEnumerable<WorldSync> Synchronize(bool withForce = false)
        {
            var sync = new List<WorldSync>();

            var worlds = _loader.Load(withForce);
            foreach (var world in worlds)
            {
                var import = importWorld(world);
                var analytics = analyzeRun(import);
                sync.Add(new WorldSync(import, analytics));
            }

            _analyticsStore.Index();

            return sync;
        }

        private void archiveWorld(WorldInfo world)
        {
            
        }

        private RunImport importWorld(WorldInfo world)
        {
            var import = _importer.Import(world);
            import = _enhancer.Enhance(import);
            _importStore.Upsert(import);
            return import;
        }

        private RunAnalytics analyzeRun(RunImport import)
        {
            var analytics = _analytics.Analyze(import);
            _analyticsStore.Upsert(analytics);
            return analytics;
        }
    }
}
