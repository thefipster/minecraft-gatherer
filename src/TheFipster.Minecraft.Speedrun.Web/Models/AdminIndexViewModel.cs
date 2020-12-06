using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class AdminIndexViewModel
    {
        public AdminIndexViewModel()
        {
        }

        public IEnumerable<RunAnalytics> Runs { get; internal set; }
    }
}