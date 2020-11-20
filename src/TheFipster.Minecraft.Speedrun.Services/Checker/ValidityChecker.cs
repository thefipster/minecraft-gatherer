using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class ValidityChecker : IValidityChecker
    {
        public ValidityResult Check(RunInfo run)
        {
            var result = new ValidityResult();

            if (run.Players.Count() == 0)
            {
                result.IsValid = false;
                result.Reasons.Add("There were no players participating.");
            }

            if (run.World.Dimensions.Count() == 0)
            {
                result.IsValid = false;
                result.Reasons.Add("The server generated no dimensions.");
            }

            if ((!run.Events.Any(x => x.Type == LogEventTypes.SetTime)) && run.Players.Count() > 1)
                result.Reasons.Add("There was no SetTime event.");

            if (run.Timings == null)
                result.Reasons.Add("Splits couldn't be determined.");

            if (run.Outcome == null)
                result.Reasons.Add("Run has no outcome.");

            if (run.Logs == null)
                result.Reasons.Add("Run has no logs.");

            return result;
        }
    }
}
