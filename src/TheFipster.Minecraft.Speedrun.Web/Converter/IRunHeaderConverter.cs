using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Converter
{
    public interface IRunListConverter
    {
        ICollection<RunHeaderViewModel> Convert(IEnumerable<RunAnalytics> analytics);
    }
}
