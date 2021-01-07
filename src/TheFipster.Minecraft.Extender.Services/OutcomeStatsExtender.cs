using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Abstractions;
using TheFipster.Minecraft.Extender.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Extender.Services
{
    public class OutcomeStatsExtender : IOutcomeStatsExtender
    {
        private readonly IOutcomeFinder _outcomeFinder;
        private readonly IPeriodSlicer _periodSlicer;

        public OutcomeStatsExtender(
            IOutcomeFinder outcomeFinder,
            IPeriodSlicer periodSlicer)
        {
            _outcomeFinder = outcomeFinder;
            _periodSlicer = periodSlicer;
        }
        public OutcomeHistogram Extend()
        {
            var periods = new List<TimePeriod>();
            var now = DateTime.UtcNow;

            periods.Add(new TimePeriod(now.AddDays(-7), now, "last week"));
            periods.Add(new TimePeriod(now.AddDays(-30), now, "last month"));
            periods.Add(new TimePeriod(now.AddDays(-90), now, "last quarter"));
            periods.Add(new TimePeriod(now.AddDays(-180), now, "last semester"));
            periods.Add(new TimePeriod(now.AddDays(-360), now, "last year"));

            var result = getPeriods(periods);
            result.Period = "relative";
            return result;
        }

        public OutcomeHistogram Extend(Periods period)
        {
            var periods = _periodSlicer.Slice(period);
            var result = getPeriods(periods);
            result.Period = period.ToString();
            return result;
        }

        private OutcomeHistogram getPeriods(IEnumerable<TimePeriod> periods)
        {
            var result = new OutcomeHistogram();
            var index = periods.Count();
            foreach (var item in periods.OrderBy(x => x.Start))
            {
                index--;

                var outcomes = _outcomeFinder
                    .Get(item.Start, item.End)
                    .Where(x => x.Feature == MetaFeatures.Outcome);

                var attempts = outcomes.Count();
                var grouped = outcomes.GroupBy(x => x.Value);

                result.Periods.Add(index);
                result.PeriodsStartedOn.Add(item.Start);
                result.Attempts.Add(attempts);
                result.Labels.Add(item.Label);

                setResultForOutome(result.Discarded, grouped, Outcomes.Discarded, attempts);
                setResultForOutome(result.Finished, grouped, Outcomes.Finished, attempts);
                setResultForOutome(result.End, grouped, Outcomes.ResetEnd, attempts);
                setResultForOutome(result.Fortress, grouped, Outcomes.ResetFortress, attempts);
                setResultForOutome(result.Nether, grouped, Outcomes.ResetNether, attempts);
                setResultForOutome(result.Search, grouped, Outcomes.ResetSearch, attempts);
                setResultForOutome(result.Started, grouped, Outcomes.ResetSpawn, attempts);
                setResultForOutome(result.Stronghold, grouped, Outcomes.ResetStronghold, attempts);
                setResultForOutome(result.Unknown, grouped, Outcomes.Unknown, attempts);
                setResultForOutome(result.Untouched, grouped, Outcomes.Untouched, attempts);
            }

            return result;
        }

        private void setResultForOutome(ICollection<double> resultCollection, IEnumerable<IGrouping<Outcomes, RunMeta<Outcomes>>> groupedOutcomes, Outcomes outcome, int totalAttempts)
        {
            var outcomes = groupedOutcomes.FirstOrDefault(x => x.Key == outcome);
            if (outcomes == null)
                resultCollection.Add(0);
            else
                resultCollection.Add(Math.Round((double)outcomes.Count() / totalAttempts * 100, 1));
        }
    }
}
