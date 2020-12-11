using System.Collections.Generic;

namespace TheFipster.Minecraft.Import.Domain
{
    public class NbtData
    {
        public NbtData()
        {
            Players = new Dictionary<string, string>();
        }

        public string Level { get; set; }
        public Dictionary<string, string> Players { get; set; }
    }
}
