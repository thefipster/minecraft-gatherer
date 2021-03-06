﻿using System.Collections.Generic;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Analytics.Abstractions
{
    public interface IPlayerAnalyzer
    {
        ICollection<PlayerAnalytics> Analyze(RunImport import);
    }
}
