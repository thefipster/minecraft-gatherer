using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface ILogFinder
    {
        IEnumerable<string> Find(DateTime date);
    }
}
