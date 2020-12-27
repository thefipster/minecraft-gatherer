using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IRenderQueue
    {
        void Enqueue(RenderJob job);
        IEnumerable<RenderJob> PeakAll();
        RenderJob Dequeue();
        RenderJob Active { get; set; }
    }
}
