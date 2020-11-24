using System.Collections.Generic;

namespace TheFipster.Minecraft.Import.Domain
{
    public class PlayerStats
    {
        public PlayerStats()
        {
            Killed = new Dictionary<string, double>();
            KilledBy = new Dictionary<string, double>();
            Used = new Dictionary<string, double>();
            Crafted = new Dictionary<string, double>();
            Custom = new Dictionary<string, double>();
            Dropped = new Dictionary<string, double>();
            Mined = new Dictionary<string, double>();
            PickedUp = new Dictionary<string, double>();
            Broken = new Dictionary<string, double>();
        }

        public Dictionary<string, double> Killed { get; set; }
        public Dictionary<string, double> KilledBy { get; set; }
        public Dictionary<string, double> Used { get; set; }
        public Dictionary<string, double> Crafted { get; set; }
        public Dictionary<string, double> Custom { get; set; }
        public Dictionary<string, double> Dropped { get; set; }
        public Dictionary<string, double> Mined { get; set; }
        public Dictionary<string, double> PickedUp { get; set; }
        public Dictionary<string, double> Broken { get; set; }
    }
}
