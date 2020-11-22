using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Modules
{
    public class SyncModule : ISyncModule
    {
        private readonly IWorldLoaderModule _worldLoaderModule;
        private readonly IImportRunModule _importRunModule;
        private readonly IAnalyticsModule _analyticsModule;

        public SyncModule(
            IWorldLoaderModule worldLoaderModule,
            IImportRunModule importRunModule,
            IAnalyticsModule analyticsModule)
        {
            _worldLoaderModule = worldLoaderModule;
            _importRunModule = importRunModule;
            _analyticsModule = analyticsModule;
        }

        public IEnumerable<RunInfo> Synchronize(bool withForce)
        {
            var runs = new List<RunInfo>();
            var worlds = _worldLoaderModule.Load(withForce);
            foreach (var world in worlds)
            {
                var run = _importRunModule.Import(world);
                var analytics = _analyticsModule.Analyze(run);
                runs.Add(run);
            }

            return runs;
        }
    }
}
