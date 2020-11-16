using System;
using System.IO;
using System.Web;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services.Extensions;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class WorldLoader : IWorldLoader
    {
        public WorldInfo Load(DirectoryInfo worldFolder)
        {
            var timestamp = getTimestamp(worldFolder);

            return new WorldInfo
            {
                Path = worldFolder.FullName,
                Name = worldFolder.Name,
                NameUrlEncoded = HttpUtility.UrlEncode(worldFolder.Name),
                CreatedOn = timestamp,
                WrittenOn = worldFolder.LastWriteTime,
                WrittenUtcOn = worldFolder.LastWriteTimeUtc,
                SizeInBytes = worldFolder.GetSize()
            };
        }

        private DateTime getTimestamp(DirectoryInfo worldFolder)
        {
            var splits = worldFolder.Name.Split('-');
            var unixEpoch = int.Parse(splits[1]);
            var timestamp = DateTimeOffset.FromUnixTimeSeconds(unixEpoch);
            return timestamp.DateTime;
        }
    }
}
