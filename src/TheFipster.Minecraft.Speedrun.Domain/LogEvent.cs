using System;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class LogEvent
    {
        public LogEventTypes Type { get; set; }
        public Player Player { get; set; }
        public LogLine Line { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }

        public static LogEvent CreateAdvancement(LogLine line) => CreateBasic(line, LogEventTypes.Advancement);
        public static LogEvent CreateSetTime(LogLine line) => CreateBasic(line, LogEventTypes.SetTime);
        public static LogEvent CreateJoined(LogLine line) => CreateBasic(line, LogEventTypes.Join);
        public static LogEvent CreateLeft(LogLine line) => CreateBasic(line, LogEventTypes.Leave);
        public static LogEvent CreateSplit(LogLine line) => CreateBasic(line, LogEventTypes.Splits);

        private static LogEvent CreateBasic(LogLine line, LogEventTypes type)
        {
            return new LogEvent
            {
                Line = line,
                Timestamp = line.Timestamp,
                Type = type
            };
        }
    }
}
