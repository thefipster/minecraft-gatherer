using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class OutcomeResult
    {
        public OutcomeResult()
        {
            SeenEndScreen = new Dictionary<string, bool>();
        }

        public OutcomeResult(Outcomes outcome) : this()
        {
            State = outcome;

            if (outcome == Outcomes.Finished)
                IsFinished = true;
        }

        public Dictionary<string, bool> SeenEndScreen { get; set; }
        public bool IsFinished { get; set; }
        public Outcomes State { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan PlayTime { get; set; }

        public bool Any(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}
