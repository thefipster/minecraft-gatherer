using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Analytics.Domain
{
    public class PlayerAnalytics : IPlayer
    {
        public PlayerAnalytics() { }

        public PlayerAnalytics(IPlayer player)
        {
            Id = player.Id;
            Name = player.Name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public PlayerStatistics Statistics { get; set; }
    }
}
