using System;
using System.Collections.Generic;
using System.Linq;
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

        public OutcomeStatsExtender(
            IOutcomeFinder outcomeFinder)
        {
            _outcomeFinder = outcomeFinder;
        }
        public OutcomeHistogram Extend()
        {
            var periods = new List<(DateTime, DateTime, string)>();
            var now = DateTime.UtcNow;

            periods.Add((now.AddDays(-7), now, "last week"));
            periods.Add((now.AddDays(-30), now, "last month"));
            periods.Add((now.AddDays(-90), now, "last quarter"));
            periods.Add((now.AddDays(-180), now, "last semester"));
            periods.Add((now.AddDays(-360), now, "last year"));

            var result = getPeriods(periods);
            result.Period = "relative";
            return result;
        }

        public OutcomeHistogram Extend(Period period)
        {
            var periods = slicePeriods(period);
            var result = getPeriods(periods);
            result.Period = period.ToString();
            return result;
        }

        private OutcomeHistogram getPeriods(IEnumerable<(DateTime, DateTime, string)> periods)
        {
            var result = new OutcomeHistogram();
            var index = periods.Count();
            foreach (var item in periods.OrderBy(x => x.Item1))
            {
                index--;

                var outcomes = _outcomeFinder
                    .Get(item.Item1, item.Item2)
                    .Where(x => x.Feature == MetaFeatures.Outcome);

                var attempts = outcomes.Count();
                var grouped = outcomes.GroupBy(x => x.Value);

                result.Periods.Add(index);
                result.PeriodsStartedOn.Add(item.Item1);
                result.Attempts.Add(attempts);
                result.Labels.Add(item.Item3);

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

        private IEnumerable<(DateTime, DateTime, string)> slicePeriods(Period period)
        {
            var periods = new List<(DateTime, DateTime, string)>();

            switch (period)
            {
                case Period.Daily:
                    {
                        var pointer = DateTime.UtcNow.Date.AddDays(-30);
                        while (pointer <= DateTime.UtcNow.Date)
                        {
                            var label = pointer.ToString("dd.MM");
                            periods.Add((pointer, pointer.AddDays(1), label));
                            pointer = pointer.AddDays(1);
                        }
                        break;
                    }
                case Period.Weekly:
                    {
                        var pointer = DateTime.UtcNow.Date.AddDays(-1 * 7 * 26);
                        pointer = getLastMondayFrom(pointer);
                        while (pointer <= DateTime.UtcNow.Date)
                        {
                            var label = pointer.ToString("dd.MM");
                            periods.Add((pointer, pointer.AddDays(7), label));
                            pointer = pointer.AddDays(7);
                        }
                        break;
                    }
                case Period.Monthly:
                    {
                        var pointer = new DateTime(DateTime.UtcNow.Year - 1, DateTime.UtcNow.Month, 1);
                        while (pointer <= DateTime.UtcNow.Date)
                        {
                            var label = pointer.ToString("MMM");
                            periods.Add((pointer, pointer.AddMonths(1), label));
                            pointer = pointer.AddMonths(1);
                        }
                        break;
                    }
            }

            return periods;

        }

        private DateTime getLastMondayFrom(DateTime referenceDate)
        {
            while (referenceDate.DayOfWeek != DayOfWeek.Monday)
                referenceDate = referenceDate.AddDays(-1);

            return referenceDate;
        }
    }
}
