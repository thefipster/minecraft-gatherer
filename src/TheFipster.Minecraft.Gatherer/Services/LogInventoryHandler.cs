using System;
using System.Collections.Generic;
using System.Text;
using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class LogInventoryHandler
    {
        private FilesystemConfig filesystemConfig;

        public LogInventoryHandler(FilesystemConfig filesystemConfig)
        {
            this.filesystemConfig = filesystemConfig;
        }

        public void Save(LogInventory inventory)
        {

        }
    }
}
