using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IRunFinder
    {
        IEnumerable<RunInfo> GetAll();

        IEnumerable<RunInfo> GetFinished();

        IEnumerable<RunInfo> GetValid();
        RunInfo GetByName(string worldName);
    }
}
