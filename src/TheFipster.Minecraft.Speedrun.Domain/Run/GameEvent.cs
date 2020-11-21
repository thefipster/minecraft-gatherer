using System;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class GameEvent
    {
        public LogEventTypes Type { get; set; }
        public Player Player { get; set; }
        public LogLine Line { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }

        public static GameEvent CreateAdvancement(LogLine line)
            => CreateBasic(line, LogEventTypes.Advancement);

        public static GameEvent CreateSetTime(LogLine line)
            => CreateBasic(line, LogEventTypes.SetTime);

        public static GameEvent CreateJoined(LogLine line)
            => CreateBasic(line, LogEventTypes.Join);

        public static GameEvent CreateLeft(LogLine line)
            => CreateBasic(line, LogEventTypes.Leave);

        public static GameEvent CreateSplit(LogLine line)
            => CreateBasic(line, LogEventTypes.Splits);

        public static GameEvent CreateDeath(LogLine line, Player player, string killer)
            => CreateWithPlayerAndReason(line, LogEventTypes.Killed, player, killer);

        public static GameEvent CreateTeleport(LogLine line, Player player, string destination)
            => CreateWithPlayerAndReason(line, LogEventTypes.Teleported, player, destination);

        public static GameEvent CreateGameMode(LogLine line, Player player, string gameMode)
            => CreateWithPlayerAndReason(line, LogEventTypes.GameMode, player, gameMode);

        public static GameEvent CreateAchievement(Player player, DateTime timestamp, string data)
            => CreateWithPlayerAndReason(timestamp, LogEventTypes.Achievement, player, data);


        private static GameEvent CreateBasic(LogLine line, LogEventTypes type) => new GameEvent
        {
            Line = line,
            Timestamp = line.Timestamp,
            Type = type
        };

        private static GameEvent CreateWithPlayerAndReason(LogLine line, LogEventTypes type, Player player, string data)
        {
            var gameEvent = CreateBasic(line, type);
            gameEvent.Player = player;
            gameEvent.Data = data;
            return gameEvent;
        }
        public static GameEvent CreateBasic(Player player, DateTime timestamp, string eventName) => new GameEvent
        {
            Data = eventName,
            Player = player,
            Timestamp = timestamp
        };

        private static GameEvent CreateWithPlayerAndReason(DateTime timestamp, LogEventTypes type, Player player, string data)
        {
            var gameEvent = CreateBasic(player, timestamp, data);
            gameEvent.Type = type;
            return gameEvent;
        }
    }
}
