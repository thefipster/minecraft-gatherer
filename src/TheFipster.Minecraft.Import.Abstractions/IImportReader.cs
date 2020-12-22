using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IImportReader
    {
        bool Exists(string name);

        int Count();

        IEnumerable<RunImport> Get();

        RunImport Get(string name);
    }
}
