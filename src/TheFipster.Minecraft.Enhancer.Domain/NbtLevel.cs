using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Enhancer.Domain
{
    public class NbtLevel
    {
        public NbtLevel()
        {
            DimensionSeeds = new Dictionary<Dimensions, long>();
        }

        public Difficulty Difficulty { get; set; }
        public long Time { get; set; }
        public GameMode GameMode { get; set; }
        public long Daytime { get; set; }
        public bool DragonKilled { get; set; }
        public string Worldname { get; set; }
        public DateTime LastPlayedOn { get; set; }
        public bool IsHardcore { get; set; }
        public Coordinate Spawn { get; set; }
        public long Seed { get; set; }
        public MinecraftVersion Version { get; set; }
        public IDictionary<Dimensions, long> DimensionSeeds { get; set; }
    }
}
