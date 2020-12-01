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

        public RunHeaderViewModel(RunAnalytics run, RunManuals manual)
        {
            Worldname = run.Worldname;
            Index = run.Index;
            PlayerIds = run.Players.Select(x => x.Id);
            Outcome = run.Outcome;

            if (run.Outcome == Outcomes.Finished)
                Runtime = run.Timings.RunTime;

            StartedOn = run.Timings.StartedOn;

            if (manual == null)
                return;

            YoutubeLink = manual.YoutubeLink;
            SpeedrunLink = manual.SpeedrunLink;
            if (manual.RuntimeInMs.HasValue)
            {
                Runtime = TimeSpan.FromMilliseconds(manual.RuntimeInMs.Value);
                RuntimeOverride = true;
            }
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
    }
}
