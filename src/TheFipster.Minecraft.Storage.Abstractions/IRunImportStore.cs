using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface IImportStore
    {
        void Insert(RunImport import);
        void Update(RunImport import);
        void Upsert(RunImport import);
        bool Exists(string name);
        int Count();
        IEnumerable<RunImport> Get();
        RunImport Get(string name);
    }
}
