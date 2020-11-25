using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules
{
    public class SyncModule : ISyncModule
    {
        private readonly IWorldLoaderModule _worldLoaderModule;
        private readonly IImportRunModule _importRunModule;

        public SyncModule(
            IWorldLoaderModule worldLoaderModule,
            IImportRunModule importRunModule)
        {
            _worldLoaderModule = worldLoaderModule;
            _importRunModule = importRunModule;
        }

        public IEnumerable<RunImport> Synchronize(bool withForce)
        {
            var runs = new List<RunImport>();
            var worlds = _worldLoaderModule.Load(withForce);
            foreach (var world in worlds)
            {
                var run = _importRunModule.Import(world);
                runs.Add(run);
            }

            return runs;
        }
    }
}
