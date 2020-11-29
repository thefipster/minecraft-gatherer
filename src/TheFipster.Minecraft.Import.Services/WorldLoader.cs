using System;
using System.IO;
using TheFipster.Minecraft.Domain.Exceptions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Import.Services.Extensions;

namespace TheFipster.Minecraft.Import.Services
{
    public class WorldLoader : IWorldLoader
    {
        public WorldInfo Load(DirectoryInfo worldFolder)
        {
            var timestamp = parseTimestamp(worldFolder);

            return new WorldInfo
            {
                Path = worldFolder.FullName,
                Name = worldFolder.Name,
                CreatedOn = timestamp,
                SizeInBytes = worldFolder.GetSize()
            };
        }

        private DateTime parseTimestamp(DirectoryInfo worldFolder)
        {
            var splits = worldFolder.Name.Split('-');

            if (splits.Length < 2)
                throw new OmittingWorldException(
                    $"Omitting world during find because it doesn't match naming pattern. {worldFolder.Name} has not two dashes.");

            if (!int.TryParse(splits[1], out var unixEpoch))
                throw new OmittingWorldException(
                    $"Omitting world during find because it doesn't match naming pattern. {worldFolder.Name} has not a valid unix epoch after the first dash.");

            return DateTimeOffset.FromUnixTimeSeconds(unixEpoch).DateTime;
        }
    }
}
