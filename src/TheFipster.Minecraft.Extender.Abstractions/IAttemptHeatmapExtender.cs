using System;
using System.Collections.Generic;
using System.Text;
using TheFipster.Minecraft.Extender.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IAttemptHeatmapExtender
    {
        Dictionary<string, DayContent> Extend();
    }
}
