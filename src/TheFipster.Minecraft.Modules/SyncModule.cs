using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Modules
{
    public class SyncModule : ISyncModule
    {
        private readonly IWorldLoaderModule _loader;
        private readonly IImportRunModule _importer;
        private readonly IEnhanceModule _enhancer;

        public SyncModule(
            IWorldLoaderModule worldLoaderModule,
            IImportRunModule importRunModule,
            IEnhanceModule enhanceModule)
        {
            _loader = worldLoaderModule;
            _importer = importRunModule;
            _enhancer = enhanceModule;
        }

        public IEnumerable<RunImport> Synchronize(bool withForce = false)
        {
            var runs = new List<RunImport>();
            var worlds = _loader.Load(withForce);
            foreach (var world in worlds)
            {
                var run = _importer.Import(world);
                run = _enhancer.Enhance(run);
                runs.Add(run);
            }

            return runs;
        }
    }
}
