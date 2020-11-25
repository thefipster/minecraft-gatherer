using System;

namespace TheFipster.Minecraft.Import.Domain
{
    public class WorldInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime CreatedOn { get; set; }
        public long SizeInBytes { get; set; }

        public override string ToString() => Name;
    }
}
