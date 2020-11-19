using System;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class OutcomeResult
    {
        public OutcomeResult()
        {

        }

        public OutcomeResult(Outcomes outcome)
        {
            State = outcome;

            if (outcome == Outcomes.Finished)
                IsFinished = true;
        }

        public bool IsFinished { get; set; }
        public Outcomes State { get; set; }
        public TimeSpan Time { get; set; }
        public TimeSpan PlayTime { get; set; }
    }
}
