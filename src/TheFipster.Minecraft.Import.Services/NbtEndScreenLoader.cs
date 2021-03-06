﻿using Cyotek.Data.Nbt;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class NbtEndScreenLoader : INbtEndScreenLoader
    {
        private const string PlayerDataFolder = "playerdata";

        public ICollection<string> Load(WorldInfo world)
        {
            var playerDataPath = Path.Combine(world.Path, PlayerDataFolder);
            var dataDir = new DirectoryInfo(playerDataPath);
            var endScreens = new List<string>();

            if (!dataDir.Exists)
                return endScreens;

            var playerFiles = dataDir.GetFiles("*.dat");
            foreach (var file in playerFiles)
            {
                var playerId = file.Name.Replace(".dat", string.Empty);

                var nbtDoc = NbtDocument.LoadDocument(file.FullName);
                var seenCreditsValue = (byte)nbtDoc.Query("seenCredits").GetValue();

                if (seenCreditsValue == 1)
                    endScreens.Add(playerId);
            }

            return endScreens;
        }
    }
}
