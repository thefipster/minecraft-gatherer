using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunHeaderViewModel
    {
        public RunHeaderViewModel() { }

        public RunHeaderViewModel(RunAnalytics analytics)
        {
            if (analytics == null)
                throw new ArgumentNullException(nameof(analytics), "Analytics data is null.");

            setAnalytics(analytics);
        }

        public RunHeaderViewModel(RunManuals manual)
        {
            if (manual == null)
                throw new ArgumentNullException(nameof(manual), "Manual Input data is null.");

            setManualInput(manual);
        }

        public RunHeaderViewModel(RunAnalytics analytics, RunManuals manual)
        {
            if (analytics == null)
                throw new ArgumentNullException(nameof(analytics), "Analytics data is null.");
            else
                setAnalytics(analytics);

            if (manual != null)
                setManualInput(manual);
        }

        public string Worldname { get; set; }
        public int Index { get; set; }

        public IEnumerable<string> PlayerIds { get; set; }

        public Outcomes Outcome { get; set; }

        public TimeSpan? Runtime { get; set; }
        public bool RuntimeOverride { get; set; }

        public DateTime StartedOn { get; set; }

        public string YoutubeLink { get; set; }
        public string SpeedrunLink { get; set; }

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

        private void setManualInput(RunManuals manual)
        {
            YoutubeLink = manual.YoutubeLink;
            SpeedrunLink = manual.SpeedrunLink;
            if (manual.RuntimeInMs.HasValue)
            {
                Runtime = TimeSpan.FromMilliseconds(manual.RuntimeInMs.Value);
                RuntimeOverride = true;
            }
        }
    }
}
