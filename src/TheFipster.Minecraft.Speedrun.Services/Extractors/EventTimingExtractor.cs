using System;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class EventTimingExtractor : IEventTimingExtractor
    {
        private TimeSpan FinishFromDragonKillPenalty = TimeSpan.FromSeconds(15);
        private TimeSpan FinishFromEventsPenalty = TimeSpan.FromMinutes(5);

        private RunInfo _run;
        private Timings _timings;

        public Timings Extract(RunInfo run)
        {
            _run = run;
            _timings = tryFindBaseTime();

            addSplit(EventNames.WeNeedToGoDeeper, SplitTypes.Nether);
            addSplit(EventNames.ATerribleFortress, SplitTypes.Fortress);
            addSplit(EventNames.IntoFire, SplitTypes.BlazeRod);
            addSplit(EventNames.GotBlazePowder, SplitTypes.BlazePowder);
            addSplit(EventNames.EyeSpy, SplitTypes.Stronghold);
            addSplit(EventNames.TheEnd, SplitTypes.TheEnd);
            addSplit(EventNames.FreeTheEnd, SplitTypes.DragonKilled);

            addFinishApproximation();

            return _timings;
        }

        private Timings tryFindBaseTime()
        {
            var timings = new Timings();

            if (_run.Events.Count() == 0)
            {
                _run.Problems.Add(new Problem("Timing not possible."));
                return new Timings("Timing not possible because there are no events.");
            }

            if (_run.Events.Any(x => x.Type == LogEventTypes.SetTime))
            {
                var setTimeEvent = _run.Events.First(x => x.Type == LogEventTypes.SetTime);
                var baseTime = setTimeEvent.Timestamp;
                return new Timings(baseTime);
            }

            if (_run.Events.Count(x => x.Type == LogEventTypes.Join) > 1)
            {
                var approxBaseTime = _run.Events.Where(x => x.Type == LogEventTypes.Join).OrderBy(x => x.Timestamp).First().Timestamp.AddSeconds(-1);
                _run.Problems.Add(new Problem("Start time approximated within seconds."));
                return new Timings(approxBaseTime, "SetTime approximated based on player join events.");
            }

            if (_run.Events.All(x => x.Line == null))
            {
                var approxBaseTime = _run.Events.Where(x => x.Type == LogEventTypes.Achievement).Min(x => x.Timestamp).AddSeconds(-30);
                _run.Problems.Add(new Problem("Start time approximated within minutes."));
                return new Timings(approxBaseTime, "SetTime approximated based on achievements.");
            }

            _run.Problems.Add(new Problem("Timing not possible."));
            return new Timings("Timing not possible because no method was applyable.");

        }

        private void addSplit(string eventName, SplitTypes splitType)
        {
            var events = _run.Events.Where(x => x.Data == eventName);
            if (!events.Any())
                return;

            var earliest = events.Min(x => x.Timestamp);
            var split = new Split(splitType, earliest);
            _timings.Splits.Add(split);
        }

        private void addFinishApproximation()
        {
            if (_timings.Splits.Any(x => x.Type == SplitTypes.DragonKilled))
            {
                var dragonSplit = _timings.Splits.First(x => x.Type == SplitTypes.DragonKilled);
                var approxFinish = dragonSplit.Timestamp + FinishFromDragonKillPenalty;
                var finishSplit = new Split(SplitTypes.ApproxFinish, approxFinish);
                _timings.Splits.Add(finishSplit);
                _timings.FinishedOn = finishSplit.Timestamp;
                _timings.Reasons.Add("Finish time approximated based on dragon death.");
                _run.Problems.Add(new Problem("Finish approximation within seconds."));
                return;
            }

            if (_run.EndScreens.Any(x => x.Value) && _run.Events.Any())
            {
                var latestEvent = _run.Events.Max(x => x.Timestamp);
                var approxFinish = latestEvent + FinishFromEventsPenalty;
                var finishSplit = new Split(SplitTypes.ApproxFinish, approxFinish);
                _timings.Splits.Add(finishSplit);
                _timings.FinishedOn = finishSplit.Timestamp;
                _timings.Reasons.Add("Finish time was approximated based on general events.");
                _run.Problems.Add(new Problem("Finish approximation within minutes."));
                return;
            }
        }
    }
}
