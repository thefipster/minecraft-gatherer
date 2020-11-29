﻿using TheFipster.Minecraft.Abstractions;

namespace TheFipster.Minecraft.Domain
{
    public class ServerProperties : IServerProperties
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
    }
}