using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Storage.Domain;

namespace TheFipster.Minecraft.Storage.Abstractions
{
    public interface IRunFinder
    {
        IEnumerable<RunAnalytics> GetAll();
        IEnumerable<RunAnalytics> GetFinished();
        IEnumerable<RunAnalytics> GetValid();
        IEnumerable<RunAnalytics> GetStarted();

        Run GetByName(string worldName);
        Run GetByIndex(int index);
    }
}
