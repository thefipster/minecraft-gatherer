using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Modules.Decorators
{
    public class ImportArchiveDecorator : IImportModule
    {
        private readonly IImportModule _component;
        private readonly IWorldArchivist _worldArchivist;
        private readonly IWorldDeleter _deleter;

        public ImportArchiveDecorator(IImportModule component, IWorldArchivist archivist, IWorldDeleter deleter)
        {
            _component = component;
            _worldArchivist = archivist;
            _deleter = deleter;
        }

        public RunImport Import(WorldInfo world)
        {
            var import = _component.Import(world);

            _worldArchivist.Compress(world.Name);
            _deleter.Rename(world.Name);

            return import;
        }
    }
}
