using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class AdminRenderJobsViewModel
    {
        public AdminRenderJobsViewModel()
        {
        }

        public IEnumerable<RenderJob> Jobs { get; internal set; }
        public RenderJob Active { get; internal set; }
        public IEnumerable<RenderResult> Results { get; internal set; }
    }
}