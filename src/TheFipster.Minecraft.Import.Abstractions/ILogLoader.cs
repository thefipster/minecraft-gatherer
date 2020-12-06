using System.Collections.Generic;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Abstractions
{
    public interface ILogLoader
    {

        IEnumerable<LogLine> Load(RunImport import);
    }
}
