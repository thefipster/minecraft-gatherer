using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunDetailViewModel
    {
        public RunDetailViewModel(RunInfo run)
        {
            Run = run;
        }

        public RunInfo Run { get; set; }
    }
}
