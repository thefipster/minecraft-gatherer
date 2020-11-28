using TheFipster.Minecraft.Abstraction;

namespace TheFipster.Minecraft.Domain
{
    public class Player : IPlayer
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
