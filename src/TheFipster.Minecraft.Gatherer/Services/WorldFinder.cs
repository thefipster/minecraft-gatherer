using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class WorldFinder
    {
        private readonly FilesystemConfig config;

        public WorldFinder(FilesystemConfig config)
        {
            this.config = config;
        }

        public string Find(LogSession session)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> findAll()
        {
            throw new NotImplementedException();
        }
    }
}
