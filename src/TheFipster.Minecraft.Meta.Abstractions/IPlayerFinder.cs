using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Abstractions
{
    public interface IPlayerFinder
    {
        ICollection<string> Find(RunImport run);
    }
}
