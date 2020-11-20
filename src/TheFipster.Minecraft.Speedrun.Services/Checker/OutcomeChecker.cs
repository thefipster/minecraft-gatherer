using System;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class OutcomeChecker : IOutcomeChecker
    {
        private readonly IPlayerNbtReader _playerReader;

        public OutcomeChecker(IPlayerNbtReader playerReader)
        {
            _playerReader = playerReader;
        }

        public OutcomeResult Check(RunInfo run)
        {
            OutcomeResult result;

            if (run.Timings != null && run.Timings.Splits.Any())
                result = outcomeFromTimings(run);
            else
                result = outcomeFromEvents(run);

            result.SeenEndScreen = _playerReader.Read(run.World);
            if (result.SeenEndScreen.Any(x => x.Value))
            {
                if (result.IsFinished == false && run.Events.Any())
                    run.Timings.FinishedOn = run.Events.OrderByDescending(x => x.Timestamp).First().Timestamp.AddMinutes(5);

                result.IsFinished = true;
                result.State = Outcomes.Finished;

            }

            return result;
        }

        private OutcomeResult outcomeFromEvents(RunInfo run)
        {
            var outcome = new OutcomeResult(Outcomes.ResetSpawn);

            if (run.Events.Any(x => x.Data == "We Need to Go Deeper"))
                outcome.State = Outcomes.ResetNether;

            if (run.Events.Any(x => x.Data == "Eye Spy"))
                outcome.State = Outcomes.ResetStronghold;

            if (run.Events.Any(x => x.Data == "The End?"))
                outcome.State = Outcomes.ResetEnd;

            if (run.Events.Any(x => x.Data == "Free the End"))
            {
                var dragonKillTime = run.Events.Where(x => x.Data == "Free the End").Select(x => x.Timestamp).Min(x => x);
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

            if (run.Timings.Splits.Any(x => x.Type == SplitTypes.ApproxFinish))
            {
                var finish = run.Timings.Splits.First(x => x.Type == SplitTypes.ApproxFinish).Timestamp;

                outcome.State = Outcomes.Finished;
                outcome.IsFinished = true;

                if (run.Timings.StartedOn.HasValue)
                    outcome.Time = finish - run.Timings.StartedOn.Value;
            }

            if (run.Events.Any())
            {
                var maxTimestamp = run.Events.Max(x => x.Timestamp);
                var minTimestamp = run.Events.Min(x => x.Timestamp);
                outcome.PlayTime = maxTimestamp - minTimestamp;
            }

            return outcome;
        }
    }
}
