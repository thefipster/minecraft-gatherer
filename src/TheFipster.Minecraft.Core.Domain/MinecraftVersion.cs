namespace TheFipster.Minecraft.Core.Domain
{
    public class MinecraftVersion
    {
        public MinecraftVersion() { }

        public MinecraftVersion(string id, string name, string snapshot)
        {
            Id = id;
            Name = name;
            Snapshot = snapshot;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Snapshot { get; set; }
    }
}
