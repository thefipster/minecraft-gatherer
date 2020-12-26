using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IRenderQueue
    {
        void Enqueue(RenderJob job);
        IEnumerable<RenderJob> PeakAll();
        RenderJob Peak();
        void Remove(string worldname);
    }
}
