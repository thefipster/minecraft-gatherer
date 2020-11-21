using System;

namespace TheFipster.Minecraft.Speedrun.Domain.Run
{
    public class RunMeta
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
    }
}
