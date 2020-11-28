using TheFipster.Minecraft.Abstraction;

namespace TheFipster.Minecraft.Analytics.Domain
{
    public class PlayerAnalytics : IPlayer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public PlayerStatistics Statistics { get; set; }
    }
}
