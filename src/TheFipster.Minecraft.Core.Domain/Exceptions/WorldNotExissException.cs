using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Core.Domain.Exceptions
{
    public class WorldNotExistsException : Exception
    {
        public WorldNotExistsException(string worldname, string worldpath)
            : base($"The world {worldname} was not found at path {worldpath}")
        { }
        public WorldNotExistsException(string worldname, IEnumerable<string> worldpaths)
            : base($"The world {worldname} was not found at the following location: {string.Join(", ", worldpaths)}")
        { }
    }
}
