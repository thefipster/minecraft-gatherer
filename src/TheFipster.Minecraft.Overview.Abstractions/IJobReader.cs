using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IJobReader
    {
        IEnumerable<RenderJob> Get();
        RenderJob Get(string worldname);
        int Count();
        bool Exists(string worldname);
    }
}
