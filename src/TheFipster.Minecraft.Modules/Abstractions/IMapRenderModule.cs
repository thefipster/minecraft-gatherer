using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Modules.Abstractions
{
    public interface IMapRenderModule
    {
        void Render(RenderJob job);
        void CreateJob(string worldname);
        IEnumerable<RenderJob> GetJobs();
        IEnumerable<RenderResult> GetResults();
    }
}
