using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain.Analytics;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ITimingStore
    {
        void Add(TimingAnalytics timings);
        void Update(TimingAnalytics timings);
        bool Exists(string worldname);
        IEnumerable<TimingAnalytics> Get();
        IEnumerable<TimingAnalytics> Get(DateTime from, DateTime to);
        IEnumerable<TimingAnalytics> Get(int lastN);
        TimingAnalytics Get(string worldname);
        int Count();
    }
}
