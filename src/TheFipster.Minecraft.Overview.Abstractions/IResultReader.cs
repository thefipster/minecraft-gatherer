using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IResultReader
    {
        IEnumerable<RenderResult> Get();
        RenderResult Get(string worldname);
        int Count();
        bool Exists(string worldname);
    }
}
