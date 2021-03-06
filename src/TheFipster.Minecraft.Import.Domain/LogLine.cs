﻿using System;

namespace TheFipster.Minecraft.Import.Domain
{
    public class LogLine
    {
        public DateTime Timestamp { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Raw { get; set; }

        public override string ToString()
            => Raw;
    }
}
