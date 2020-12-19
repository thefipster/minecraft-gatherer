﻿using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface IWorldFinder
    {
        FileSystemInfo Find(string worldname);
        Dictionary<Locations, FileSystemInfo> Locate(string worldname);
    }
}
