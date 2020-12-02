namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IServerProperties
    {
        long? LevelSeed { get; set; }
        string GameMode { get; set; }
        string LevelName { get; set; }
        string MessageOfTheDay { get; set; }
        string Difficulty { get; set; }
        bool IsOnlineMode { get; set; }
        int ViewDistance { get; set; }
        bool IsHardcore { get; set; }
        bool IsConsoleBroadcastEnabled { get; set; }
        bool IsSpawnMonstersEnabled { get; set; }
        bool IsSpawnAnimalsEnabled { get; set; }
        int SpawnProtection { get; set; }
        bool IsSpawnNpcEnabled { get; set; }
    }
}
