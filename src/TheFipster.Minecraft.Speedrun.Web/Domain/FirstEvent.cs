using System;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Web.Domain
{
    public class FirstEvent
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public Player Player { get; set; }
    }
}
