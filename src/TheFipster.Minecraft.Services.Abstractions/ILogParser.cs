﻿using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface ILogParser
    {
        IEnumerable<LogLine> Read(IEnumerable<string> entries, DateTime date);
    }
}
