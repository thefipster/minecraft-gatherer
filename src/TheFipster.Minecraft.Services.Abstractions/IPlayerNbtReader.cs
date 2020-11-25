﻿using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Services.Abstractions
{
    public interface IPlayerNbtReader
    {
        ICollection<string> Read(WorldInfo world);
    }
}
