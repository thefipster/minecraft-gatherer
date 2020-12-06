using System.Collections.Generic;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface IManualsStore
    {
        void Insert(RunManuals manuals);
        void Update(RunManuals manuals);
        void Upsert(RunManuals manuals);
        bool Exists(string name);
        int Count();
        IEnumerable<RunManuals> Get();
        RunManuals Get(string name);
    }
}
