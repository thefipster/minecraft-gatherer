using System;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class EventTimingExtractor : IEventTimingExtractor
    {
        private RunInfo _run;
        private Timings _timings;

        public Timings Extract(RunInfo run)
        {
            _run = run;
            _timings = tryFindBaseTime();

            addSplit("We Need to Go Deeper", SplitTypes.Nether);
            addSplit("A Terrible Fortress", SplitTypes.Fortress);
            addSplit("Into Fire", SplitTypes.BlazeRod);
            addSplit("Got Blaze Powder", SplitTypes.BlazePowder);
            addSplit("Eye Spy", SplitTypes.Stronghold);
            addSplit("The End?", SplitTypes.TheEnd);
            addSplit("Free the End", SplitTypes.DragonKilled);

            addFinishApproximation();

            return _timings;
        }

        private Timings tryFindBaseTime()
        {
            var timings = new Timings();

            if (_run.Events.Count() == 0)
            {
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
                return new Timings(approxBaseTime, "SetTime approximated based on player join events.");
            }

            if (_run.Events.All(x => x.Line == null))
            {
                var approxBaseTime = _run.Events.Where(x => x.Type == LogEventTypes.Achievement).Min(x => x.Timestamp).AddSeconds(-30);
                return new Timings(approxBaseTime, "SetTime approximated based on achievements.");
            }

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
                var finishSplit = new Split(SplitTypes.ApproxFinish, dragonSplit.Timestamp + TimeSpan.FromSeconds(15));
                _timings.Splits.Add(finishSplit);
                _timings.FinishedOn = finishSplit.Timestamp;
            }
        }
    }
}
