using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Storage.Abstractions;
using TheFipster.Minecraft.Storage.Domain;

namespace TheFipster.Minecraft.Storage.Services
{
    public class RunFinder : IRunFinder
    {
        private readonly IImportStore _importStore;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IManualsStore _manualsStore;

        public RunFinder(
            IImportStore importStore,
            IAnalyticsStore analyticsStore,
            IManualsStore manualsStore)
        {
            _importStore = importStore;
            _analyticsStore = analyticsStore;
            _manualsStore = manualsStore;
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
            run = fillRun(run);

            return run;
        }

        public Run GetByName(string worldName)
        {
            var run = new Run();
            run.Analytics = _analyticsStore.Get(worldName);
            run = fillRun(run);

            return run;
        }

        private Run fillRun(Run run)
        {
            run.Import = _importStore.Get(run.Analytics.Worldname);
            run.Manuals = _manualsStore.Get(run.Analytics.Worldname);

            if (run.Manuals != null)
                adjustRuntime(run);

            return run;
        }

        private static void adjustRuntime(Run run)
        {
            if (run.Manuals.RuntimeInMs.HasValue)
                run.Analytics.Timings.RunTime = TimeSpan.FromMilliseconds(run.Manuals.RuntimeInMs.Value);
        }
    }
}
