using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class OutcomeChecker : IOutcomeChecker
    {
        public OutcomeResult Check(RunInfo run)
        {
            if (run.Splits.Any(x => x.Type == SplitTypes.ApproxFinish))
            {
                var outcome = new OutcomeResult(Outcomes.Finished);
                outcome.Time = run.Splits.First(x => x.Type == SplitTypes.ApproxFinish).Timestamp;
                return outcome;
            }

            if (run.Splits.Any(x => x.Type == SplitTypes.TheEnd))
                return new OutcomeResult(Outcomes.ResetEnd);

            if (run.Splits.Any(x => x.Type == SplitTypes.Stronghold))
                return new OutcomeResult(Outcomes.ResetStronghold);

            if (run.Splits.Any(x => x.Type == SplitTypes.Nether))
                return new OutcomeResult(Outcomes.ResetNether);

            return new OutcomeResult(Outcomes.ResetSpawn);
        }
    }
}
