using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class EventSplitExtractor : IEventSplitExtractor
    {
        private DateTime _baseTime;

        public List<Split> Extract(IEnumerable<GameEvent> events)
        {
            var splits = new List<Split>();

            if (tryFindBaseTime(events, out var baseTime))
                _baseTime = baseTime;
            else
                return splits;

            addNetherSplitIfFound(events, splits);
            addFortressSplitIfFound(events, splits);
            addBlazeRodSplitIfFound(events, splits);
            addStrongholdSplitIfFound(events, splits);
            addTheEndSplitIfFound(events, splits);
            addFinishSplitsIfFound(events, splits);

            return splits;

        }

        private bool tryFindBaseTime(IEnumerable<GameEvent> events, out DateTime baseTime)
        {
            var setTimeEvent = events.FirstOrDefault(x => x.Type == LogEventTypes.SetTime);

            if (setTimeEvent == null)
            {
                baseTime = DateTime.UtcNow;
                return false;
            }

            baseTime = setTimeEvent.Timestamp;
            return true;
        }

        private void addNetherSplitIfFound(IEnumerable<GameEvent> events, List<Split> splits)
        {
            var netherEventss = events.Where(x => x.Type == LogEventTypes.Advancement && x.Data == "We Need to Go Deeper");
            if (!netherEventss.Any())
                return;

            var earliest = netherEventss.Min(x => x.Timestamp);
            var split = new Split(SplitTypes.Nether, earliest - _baseTime);
            splits.Add(split);
        }

        private void addFortressSplitIfFound(IEnumerable<GameEvent> events, List<Split> splits)
        {
            var fortressEvents = events.Where(x => x.Type == LogEventTypes.Advancement && x.Data == "A Terrible Fortress");
            if (!fortressEvents.Any())
                return;

            var earliest = fortressEvents.Min(x => x.Timestamp);
            var split = new Split(SplitTypes.Fortress, earliest - _baseTime);
            splits.Add(split);
        }

        private void addBlazeRodSplitIfFound(IEnumerable<GameEvent> events, List<Split> splits)
        {
            var blazeRodEvents = events.Where(x => x.Type == LogEventTypes.Advancement && x.Data == "Into Fire");
            if (!blazeRodEvents.Any())
                return;

            var earliest = blazeRodEvents.Min(x => x.Timestamp);
            var split = new Split(SplitTypes.BlazeRod, earliest - _baseTime);
            splits.Add(split);
        }

        private void addStrongholdSplitIfFound(IEnumerable<GameEvent> events, List<Split> splits)
        {
            var strongholdEvents = events.Where(x => x.Type == LogEventTypes.Advancement && x.Data == "Eye Spy");
            if (!strongholdEvents.Any())
                return;

            var earliest = strongholdEvents.Min(x => x.Timestamp);
            var split = new Split(SplitTypes.Stronghold, earliest - _baseTime);
            splits.Add(split);
        }

        private void addTheEndSplitIfFound(IEnumerable<GameEvent> events, List<Split> splits)
        {
            var theEndEvents = events.Where(x => x.Type == LogEventTypes.Advancement && x.Data == "The End?");
            if (!theEndEvents.Any())
                return;

            var earliest = theEndEvents.Min(x => x.Timestamp);
            var split = new Split(SplitTypes.TheEnd, earliest - _baseTime);
            splits.Add(split);
        }

        private void addFinishSplitsIfFound(IEnumerable<GameEvent> events, List<Split> splits)
        {
            var finishEvents = events.Where(x => x.Type == LogEventTypes.Advancement && x.Data == "Free the End");
            if (!finishEvents.Any())
                return;

            var earliest = finishEvents.Min(x => x.Timestamp);
            var dragonSplit = new Split(SplitTypes.DragonKilled, earliest - _baseTime);
            var finishSplit = new Split(SplitTypes.ApproxFinish, dragonSplit.Timestamp + TimeSpan.FromSeconds(15));
            splits.Add(dragonSplit);
            splits.Add(finishSplit);
        }
    }
}
