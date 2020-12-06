using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Core.Domain
{
    public class Player : IPlayer
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
