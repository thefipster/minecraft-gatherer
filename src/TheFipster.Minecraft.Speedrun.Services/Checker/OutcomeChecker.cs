using System;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class OutcomeChecker : IOutcomeChecker
    {
        public OutcomeResult Check(RunInfo run)
        {
            OutcomeResult result;

            if (run.Timings != null && run.Timings.Splits.Any())
                result = outcomeFromTimings(run);
            else
                result = outcomeFromEvents(run);

            return result;
        }

        private OutcomeResult outcomeFromEvents(RunInfo run)
        {
            var outcome = new OutcomeResult(Outcomes.ResetSpawn);

            if (run.Events.Any(x => x.Data == EventNames.WeNeedToGoDeeper))
                outcome.State = Outcomes.ResetNether;

            if (run.Events.Any(x => x.Data == EventNames.EyeSpy))
                outcome.State = Outcomes.ResetStronghold;

            if (run.Events.Any(x => x.Data == EventNames.TheEnd))
                outcome.State = Outcomes.ResetEnd;

            if (run.Events.Any(x => x.Data == EventNames.FreeTheEnd))
            {
                var dragonKillTime = run.Events.Where(x => x.Data == EventNames.FreeTheEnd).Select(x => x.Timestamp).Min(x => x);
                var worldCreationTime = run.World.CreatedOn.ToLocalTime();
                var delta = dragonKillTime - worldCreationTime - TimeSpan.FromSeconds(15);

                outcome.State = Outcomes.Finished;
                outcome.IsFinished = true;
                outcome.Time = delta;
            }

            if (run.Events.Any())
            {
                var maxTimestamp = run.Events.Max(x => x.Timestamp);
                var minTimestamp = run.Events.Min(x => x.Timestamp);
                outcome.PlayTime = maxTimestamp - minTimestamp;
            }

            return outcome;
        }

        private OutcomeResult outcomeFromTimings(RunInfo run)
        {
            var outcome = new OutcomeResult(Outcomes.ResetSpawn);

            if (run.Timings.StartedOn.HasValue)
                outcome.State = Outcomes.ResetSpawn;

            if (run.Timings.Splits.Any(x => x.Type == SplitTypes.Nether))
                outcome.State = Outcomes.ResetNether;

            if (run.Timings.Splits.Any(x => x.Type == SplitTypes.BlazePowder))
                outcome.State = Outcomes.ResetSearch;

            if (run.Timings.Splits.Any(x => x.Type == SplitTypes.Stronghold))
                outcome.State = Outcomes.ResetStronghold;

            if (run.Timings.Splits.Any(x => x.Type == SplitTypes.TheEnd))
                outcome.State = Outcomes.ResetEnd;

            if (run.Timings.FinishedOn.HasValue)
            {
                outcome.State = Outcomes.Finished;
                outcome.IsFinished = true;

                if (run.Timings.StartedOn.HasValue)
                    outcome.Time = run.Timings.FinishedOn.Value - run.Timings.StartedOn.Value;
            }

            outcome.PlayTime = getPlaytime(run);

            return outcome;
        }

        private TimeSpan getPlaytime(RunInfo run)
        {
            var maxTimestamp = DateTime.MinValue;
            var minTimestamp = DateTime.MaxValue;
            if (run.Events.Any())
            {
                maxTimestamp = run.Events.Max(x => x.Timestamp);
                minTimestamp = run.Events.Min(x => x.Timestamp);
            }

            if (run.Timings.FinishedOn.HasValue && maxTimestamp < run.Timings.FinishedOn.Value)
                maxTimestamp = run.Timings.FinishedOn.Value;

            if (run.Timings.StartedOn.HasValue && minTimestamp > run.Timings.StartedOn.Value)
                minTimestamp = run.Timings.StartedOn.Value;

            return maxTimestamp - minTimestamp;
        }
    }
}
