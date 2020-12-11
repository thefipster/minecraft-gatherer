using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Enhancer.Domain
{
    public class NbtPlayer
    {
        public NbtPlayer() { }

        public NbtPlayer(string playerId)
        {
            PlayerId = playerId;
        }

        public string PlayerId { get; set; }
        public long HurtTimestamp { get; set; }
        public long SleepTimer { get; set; }
        public int AbsorptionAmount { get; set; }
        public double FallDistance { get; set; }
        public long DeathTime { get; set; }
        public int XpSeed { get; set; }
        public int XpTotal { get; set; }
        public bool SeenCredits { get; set; }
        public int Health { get; set; }
        public int FoodSaturationLevel { get; set; }
        public int XpLevel { get; set; }
        public long Score { get; set; }
        public Coordinate Spawn { get; set; }
    }
}
