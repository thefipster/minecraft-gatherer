using System;

namespace TheFipster.Minecraft.Meta.Domain
{
    public class RunEvent
    {
        public EventTypes Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string PlayerId { get; set; }
        public string Value { get; set; }
    }
}
