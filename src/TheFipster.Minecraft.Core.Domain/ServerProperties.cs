namespace TheFipster.Minecraft.Core.Domain
{
    public class ServerProperties
    {
        public long? LevelSeed { get; set; }
        public string GameMode { get; set; }
        public string LevelName { get; set; }
        public string MessageOfTheDay { get; set; }
        public string Difficulty { get; set; }
        public bool IsOnlineMode { get; set; }
        public int ViewDistance { get; set; }
        public bool IsHardcore { get; set; }
        public bool IsConsoleBroadcastEnabled { get; set; }
        public bool IsSpawnMonstersEnabled { get; set; }
        public bool IsSpawnAnimalsEnabled { get; set; }
        public int SpawnProtection { get; set; }
        public bool IsSpawnNpcEnabled { get; set; }
        public bool IsWhitelisted { get; set; }
        public bool IsPvp { get; set; }
    }
}
