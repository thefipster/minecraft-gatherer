using System;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class WorldInfo
    {
        public string Path { get; set; }
        public DateTime CreatedOn { get; set; }
        public long SizeInBytes { get; set; }
    }
}
