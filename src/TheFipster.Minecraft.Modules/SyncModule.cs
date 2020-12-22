using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Abstractions;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Modules.Models;

namespace TheFipster.Minecraft.Modules
{
    public class SyncModule : ISyncModule
    {
        private readonly IWorldLoaderModule _loader;
        private readonly IImportModule _importer;
        private readonly IEnhanceModule _enhancer;
        private readonly IImportWriter _importWriter;
        private readonly IAnalyticsModule _analytics;
        private readonly IAnalyticsWriter _analyticsWriter;
        private readonly IRunIndexer _runIndexer;

        public SyncModule(
            IWorldLoaderModule worldLoaderModule,
            IImportModule importModule,
            IEnhanceModule enhanceModule,
            IImportWriter importWriter,
            IAnalyticsModule analyticsModule,
            IAnalyticsWriter analyticsWriter,
            IRunIndexer runIndexer)
        {
            _loader = worldLoaderModule;
            _importer = importModule;
            _enhancer = enhanceModule;
            _analytics = analyticsModule;
            _importWriter = importWriter;
            _analyticsWriter = analyticsWriter;
            _runIndexer = runIndexer;
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

            _runIndexer.Index();

            return sync;
        }

        private RunImport importWorld(WorldInfo world)
        {
            var import = _importer.Import(world);
            import = _enhancer.Enhance(import);
            _importWriter.Upsert(import);
            return import;
        }

        private RunAnalytics analyzeRun(RunImport import)
        {
            var analytics = _analytics.Analyze(import);
            _analyticsWriter.Upsert(analytics);
            return analytics;
        }
    }
}
