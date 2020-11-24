using System;

namespace TheFipster.Minecraft.Import.Domain
{
    public class WorldInfo
    {
        public string Path { get; set; }
        public DateTime CreatedOn { get; set; }
        public long SizeInBytes { get; set; }
        public string Name { get; set; }
        public DateTime WrittenOn { get; set; }
        public DateTime WrittenUtcOn { get; set; }
        public string NameUrlEncoded { get; set; }
    }
}
