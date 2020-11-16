using System;
using System.IO;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class WorldLoadVerifier : IWorldLoader
    {
        private IWorldLoader _component;

        public WorldLoadVerifier(IWorldLoader component)
            => _component = component;

        public WorldInfo Load(DirectoryInfo worldFolder)
        {
            var splits = worldFolder.Name.Split('-');

            if (splits.Length < 2)
                throw new Exception(
                    $"Omitting world during find because it doesn't match naming pattern. {worldFolder.Name} has not two dashes.");

            if (!int.TryParse(splits[1], out var unixEpoch))
                throw new Exception(
                    $"Omitting world during find because it doesn't match naming pattern. {worldFolder.Name} has not a valid unix epoch after the first dash.");

            return _component.Load(worldFolder);
        }
    }
}
