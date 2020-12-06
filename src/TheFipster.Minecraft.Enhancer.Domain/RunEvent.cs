using System;

namespace TheFipster.Minecraft.Enhancer.Domain
{
    public class RunEvent
    {
        public RunEvent() { }

        public RunEvent(EventTypes type)
            => Type = type;

        public RunEvent(EventTypes type, DateTime timestamp)
            : this(type)
            => Timestamp = timestamp;

        public RunEvent(EventTypes type, DateTime timestamp, string value)
            : this(type, timestamp)
            => Value = value;

        public RunEvent(EventTypes type, DateTime timestamp, string value, string playerId)
            : this(type, timestamp, value)
            => PlayerId = playerId;

        public EventTypes Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string PlayerId { get; set; }
        public string Value { get; set; }

        public override string ToString() => $"{Timestamp:yyyy-MM-dd HH:mm:ss} - {Type} - {Value} - {PlayerId}";
    }
}
