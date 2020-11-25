using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IRunImportStore
    {
        void Add(RunImport run);
        void Update(RunImport run);
        bool Exists(string name);
        int Count();
        IEnumerable<RunImport> Get();
        RunImport Get(string name);
    }
}
