using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;

namespace TheFipster.Minecraft.Core.Services
{
    public class OpsReader : IOpsReader
    {
        private const string OpsFilename = "ops.json";

        private readonly FileInfo opsFile;

        public OpsReader(IConfigService config)
        {
            var filepath = Path.Combine(config.ServerLocation.FullName, OpsFilename);
            opsFile = new FileInfo(filepath);
        }
        public bool Exists(string id)
        {
            var playerId = Guid.Parse(id);
            var players = Read();

            foreach (var player in players)
                if (Guid.Parse(player.Id) == playerId)
                    return true;

            return false;
        }

        public IEnumerable<IPlayer> Read()
        {
            var json = File.ReadAllText(opsFile.FullName);
            var players = JsonConvert.DeserializeObject<IEnumerable<ServerPlayer>>(json);
            return players;
        }
    }
}
