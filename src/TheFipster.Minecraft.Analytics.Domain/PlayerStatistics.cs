using System.Collections.Generic;

namespace TheFipster.Minecraft.Analytics.Domain
{
    public class PlayerStatistics
    {
        public Dictionary<string, int> Broken { get; set; }
        public Dictionary<string, int> Crafted { get; set; }
        public Dictionary<string, int> Dropped { get; set; }
        public Dictionary<string, int> Killed { get; set; }
        public Dictionary<string, int> KilledBy { get; set; }
        public Dictionary<string, int> Mined { get; set; }
        public Dictionary<string, int> PickedUp { get; set; }
        public Dictionary<string, int> Used { get; set; }
    }
}
