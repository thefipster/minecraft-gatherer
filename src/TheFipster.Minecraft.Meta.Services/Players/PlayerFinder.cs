using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Players
{
    public class PlayerFinder : IPlayerFinder
    {
        public ICollection<string> Find(RunImport run)
            => new List<string>();
    }
}
