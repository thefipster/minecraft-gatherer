using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class WorldInfo
    {
        public WorldInfo()
        {
            Dimensions = new List<DimensionInfo>();
        }

        public string Path { get; set; }
        public DateTime CreatedOn { get; set; }
        public long SizeInBytes { get; set; }
        public string Name { get; set; }
        public DateTime WrittenOn { get; set; }
        public DateTime WrittenUtcOn { get; set; }
        public List<DimensionInfo> Dimensions { get; set; }
        public string NameUrlEncoded { get; set; }
    }
}
