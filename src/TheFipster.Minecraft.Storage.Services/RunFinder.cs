using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Storage.Abstractions;
using TheFipster.Minecraft.Storage.Domain;

namespace TheFipster.Minecraft.Storage.Services
{
    public class RunFinder : IRunFinder
    {
        private readonly IRunImportStore _importStore;
        private readonly IRunAnalyticsStore _analyticsStore;

        public RunFinder(
            IRunImportStore importStore,
            IRunAnalyticsStore analyticsStore)
        {
            _importStore = importStore;
            _analyticsStore = analyticsStore;
        }

        public IEnumerable<RunAnalytics> GetAll() => _analyticsStore
            .Get();

        public IEnumerable<RunAnalytics> GetFinished() => _analyticsStore
            .Get()
            .Where(x => x.Outcome == Outcomes.Finished);

        public IEnumerable<RunAnalytics> GetStarted() => GetValid()
            .Where(x => x.Players.Count > 2);

        public IEnumerable<RunAnalytics> GetValid() => _analyticsStore
            .Get()
            .Where(x => x.Outcome != Outcomes.Unknown && x.Outcome != Outcomes.Untouched);

        public Run GetByIndex(int index)
        {
            var run = new Run();
            run.Analytics = _analyticsStore.Get(index);
            run.Import = _importStore.Get(run.Analytics.Worldname);
            return run;
        }

        public Run GetByName(string worldName)
        {
            var run = new Run();
            run.Import = _importStore.Get(worldName);
            run.Analytics = _analyticsStore.Get(worldName);
            return run;
        }
    }
}
