using System.Collections.Generic;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Manual.Abstractions
{
    public interface IManualsReader
    {
        int Count();
        IEnumerable<RunManuals> Get();
        RunManuals Get(string name);
    }
}
