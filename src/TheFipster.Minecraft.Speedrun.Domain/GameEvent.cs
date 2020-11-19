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

        public static GameEvent CreateAdvancement(LogLine line) => CreateBasic(line, LogEventTypes.Advancement);
        public static GameEvent CreateSetTime(LogLine line) => CreateBasic(line, LogEventTypes.SetTime);
        public static GameEvent CreateJoined(LogLine line) => CreateBasic(line, LogEventTypes.Join);
        public static GameEvent CreateLeft(LogLine line) => CreateBasic(line, LogEventTypes.Leave);
        public static GameEvent CreateSplit(LogLine line) => CreateBasic(line, LogEventTypes.Splits);

        public static GameEvent CreateBasic(Player player, DateTime timestamp, string eventName) => new GameEvent
        {
            Data = eventName,
            Player = player,
            Timestamp = timestamp
        };

        public static GameEvent CreateAchievement(Player player, DateTime timestamp, string data) => new GameEvent
        {
            Data = data,
            Player = player,
            Timestamp = timestamp,
            Type = LogEventTypes.Achievement
        };

        private static GameEvent CreateBasic(LogLine line, LogEventTypes type) => new GameEvent
        {
            Line = line,
            Timestamp = line.Timestamp,
            Type = type
        };
    }
}
