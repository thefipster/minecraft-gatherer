using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class RunAdminViewModel
    {
        public RunAdminViewModel() { }

        public RunAdminViewModel(RunAnalytics analytics)
        {
            if (analytics == null)
                throw new ArgumentNullException(nameof(analytics), "Analytics data is null.");

            setAnalytics(analytics);
        }

        public string Worldname { get; set; }
        public int Index { get; set; }
        public IEnumerable<string> PlayerIds { get; set; }
        public Outcomes Outcome { get; set; }
        public TimeSpan? Runtime { get; set; }
        public DateTime StartedOn { get; set; }
        public bool HasPendingRenderJob { get; internal set; }
        public bool HasRenderedMap { get; internal set; }

        private void setAnalytics(RunAnalytics analytics)
        {
            Worldname = analytics.Worldname;
            Index = analytics.Index;
            PlayerIds = analytics.Players.Select(x => x.Id);
            Outcome = analytics.Outcome;

            if (analytics.Outcome == Outcomes.Finished)
                Runtime = analytics.Timings.RunTime;

            StartedOn = analytics.Timings.StartedOn;
        }
    }
}