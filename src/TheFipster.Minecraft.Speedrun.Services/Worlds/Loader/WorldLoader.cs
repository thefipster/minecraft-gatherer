using System;
using System.IO;
using System.Linq;
using System.Web;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class WorldLoader : IWorldLoader
    {
        public WorldInfo Load(DirectoryInfo worldFolder)
        {
            var size = getSize(worldFolder);
            var timestamp = getTimestamp(worldFolder);

            return new WorldInfo
            {
                Path = worldFolder.FullName,
                Name = worldFolder.Name,
                NameUrlEncoded = HttpUtility.UrlEncode(worldFolder.Name),
                CreatedOn = timestamp,
                WrittenOn = worldFolder.LastWriteTime,
                WrittenUtcOn = worldFolder.LastWriteTimeUtc,
                SizeInBytes = size
            };
        }

        private static long getSize(DirectoryInfo worldFolder)
        {
            return worldFolder
                .EnumerateFiles("*", SearchOption.AllDirectories)
                .Sum(file => file.Length);
        }

        private static DateTime getTimestamp(DirectoryInfo worldFolder)
        {
            var splits = worldFolder.Name.Split('-');
            var unixEpoch = int.Parse(splits[1]);
            var createdDate = DateTimeOffset.FromUnixTimeSeconds(unixEpoch);
            return createdDate.DateTime;
        }
    }
}
