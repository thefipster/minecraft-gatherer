using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface ILogFinder
    {
        IEnumerable<string> Find(DateTime date);
    }
}
