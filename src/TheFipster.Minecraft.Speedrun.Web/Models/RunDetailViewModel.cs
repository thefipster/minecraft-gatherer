using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Web.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunDetailViewModel
    {
        public RunDetailViewModel(RunInfo run)
        {
            Run = run;
        }

        public RunInfo Run { get; set; }

        public DateTime? BaseTime
        {
            get
            {
                var startEvent = Run.Events.FirstOrDefault(x => x.Type == LogEventTypes.SetTime);
                if (startEvent == null)
                    return null;

                return startEvent.Timestamp;

            }
        }

        public IEnumerable<FirstEvent> FirstAdvancement { get; internal set; }
    }
}
