using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Extender.Services
{
    public class PersonalBestExtender : IPersonalBestExtender
    {
        private readonly IRunFinder _finder;

        public PersonalBestExtender(IRunFinder finder)
        {
            _finder = finder;
        }

        public IEnumerable<RunAnalytics> Extend(int n)
        {
            var finishedRuns = _finder.GetFinished().OrderBy(x => x.Timings.StartedOn);
            var pbTime = TimeSpan.MaxValue;

            var pbs = new List<RunAnalytics>();

            foreach (var run in finishedRuns)
            {
                if (run.Timings.RunTime < pbTime)
                {
                    pbTime = run.Timings.RunTime;
                    pbs.Add(run);
                }
            }

            if (n > 0)
                return pbs.OrderByDescending(x => x.Timings.StartedOn).Take(n);

            return pbs;
        }
    }
}
