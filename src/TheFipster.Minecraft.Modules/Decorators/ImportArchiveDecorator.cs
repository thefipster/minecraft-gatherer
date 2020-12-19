using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules.Decorators
{
    public class ImportArchiveDecorator : IImportModule
    {
        private readonly IImportModule _component;
        private readonly IWorldArchivist _worldArchivist;

        public ImportArchiveDecorator(IImportModule component, IWorldArchivist worldArchivist)
        {
            _component = component;
            _worldArchivist = worldArchivist;
        }

        public RunImport Import(WorldInfo world)
        {
            var import = _component.Import(world);

            _worldArchivist.Compress(world.Name);

            return import;
        }
    }
}
