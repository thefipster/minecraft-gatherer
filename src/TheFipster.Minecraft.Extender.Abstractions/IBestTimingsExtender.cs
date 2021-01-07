using System.Collections.Generic;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Extender.Domain;

namespace TheFipster.Minecraft.Extender.Abstractions
{
    public interface IBestTimingsExtender
    {
        Dictionary<Sections, RunTiming> Extend();
    }
}
