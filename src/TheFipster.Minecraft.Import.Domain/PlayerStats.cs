using System.Collections.Generic;

namespace TheFipster.Minecraft.Import.Domain
{
    public class Stats
    {
        public Stats()
        {
            Killed = new Dictionary<string, int>();
            KilledBy = new Dictionary<string, int>();
            Used = new Dictionary<string, int>();
            Crafted = new Dictionary<string, int>();
            Custom = new Dictionary<string, int>();
            Dropped = new Dictionary<string, int>();
            Mined = new Dictionary<string, int>();
            PickedUp = new Dictionary<string, int>();
            Broken = new Dictionary<string, int>();
        }

        public Dictionary<string, int> Killed { get; set; }
        public Dictionary<string, int> KilledBy { get; set; }
        public Dictionary<string, int> Used { get; set; }
        public Dictionary<string, int> Crafted { get; set; }
        public Dictionary<string, int> Custom { get; set; }
        public Dictionary<string, int> Dropped { get; set; }
        public Dictionary<string, int> Mined { get; set; }
        public Dictionary<string, int> PickedUp { get; set; }
        public Dictionary<string, int> Broken { get; set; }
    }
}
