using Cyotek.Data.Nbt;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class PlayerNbtReader : IPlayerNbtReader
    {
        private const string PlayerDataFolder = "playerdata";
        private readonly IConfigService _config;

        public PlayerNbtReader(IConfigService config)
        {
            _config = config;
        }

        public Dictionary<string, bool> Read(WorldInfo world)
        {
            var playerDataPath = Path.Combine(_config.ServerLocation.FullName, world.Name, PlayerDataFolder);
            var dataDir = new DirectoryInfo(playerDataPath);

            if (!dataDir.Exists)
                return new Dictionary<string, bool>();

            var playerFiles = dataDir.GetFiles("*.dat");
            var seenCreditMap = new Dictionary<string, bool>();

            foreach (var file in playerFiles)
            {
                var playerId = file.Name.Replace(".dat", string.Empty);

                var nbtDoc = NbtDocument.LoadDocument(file.FullName);
                var seenCreditsValue = (byte)nbtDoc.Query("seenCredits").GetValue();

                if (seenCreditsValue == 0)
                    seenCreditMap.Add(playerId, false);
                else
                    seenCreditMap.Add(playerId, true);
            }

            return seenCreditMap;
        }
    }
}
