using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;
using TheFipster.Minecraft.Storage.Domain;

namespace TheFipster.Minecraft.Storage.Services
{
    public class RunFinder : IRunFinder
    {
        private readonly IImportStore _importStore;
        private readonly IAnalyticsStore _analyticsStore;
        private readonly IManualsReader _manualsReader;
        private readonly IWorldFinder _worldFinder;

        public RunFinder(
            IImportStore importStore,
            IAnalyticsStore analyticsStore,
            IManualsReader manualsReader,
            IWorldFinder worldFinder)
        {
            _importStore = importStore;
            _analyticsStore = analyticsStore;
            _manualsReader = manualsReader;
            _worldFinder = worldFinder;
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
            run.Manuals = _manualsReader.Get(run.Analytics.Worldname);
            run.Locations = _worldFinder.Locate(run.Analytics.Worldname);

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
