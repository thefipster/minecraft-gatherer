using System.IO;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;
using TheFipster.Minecraft.Speedrun.Services.Extensions;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class WorldDimensionLoader : IWorldLoader
    {
        private readonly IWorldLoader _component;

        public WorldDimensionLoader(IWorldLoader component)
        {
            _component = component;
        }

        public WorldInfo Load(DirectoryInfo worldFolder)
        {
            var worldInfo = _component.Load(worldFolder);

            if (tryLoadOverworld(worldFolder, out var overworld))
                worldInfo.Dimensions.Add(overworld);

            if (tryLoadNether(worldFolder, out var nether))
                worldInfo.Dimensions.Add(nether);

            if (tryLoadTheEnd(worldFolder, out var theEnd))
                worldInfo.Dimensions.Add(theEnd);

            return worldInfo;
        }

        private bool tryLoadOverworld(DirectoryInfo worldFolder, out DimensionInfo overworld)
        {
            overworld = new DimensionInfo("Overworld");

            var regionFolder = worldFolder.GetDirectories("region");
            if (regionFolder.Any())
                overworld.Size += regionFolder.First().GetSize();

            var poiFolder = worldFolder.GetDirectories("poi");
            if (poiFolder.Any())
                overworld.Size += poiFolder.First().GetSize();

            if (overworld.Size != 0)
                return true;

            return false;
        }

        private bool tryLoadNether(DirectoryInfo worldFolder, out DimensionInfo nether)
        {
            nether = new DimensionInfo("Nether");

            var netherFolder = worldFolder.GetDirectories("DIM-1");
            if (!netherFolder.Any())
                return false;

            var regionFolder = netherFolder.First().GetDirectories("region");
            if (!regionFolder.Any())
                return false;

            nether.Size = netherFolder.First().GetSize();
            return true;
        }

        private bool tryLoadTheEnd(DirectoryInfo worldFolder, out DimensionInfo theEnd)
        {
            theEnd = new DimensionInfo("The End");

            var theEndFolder = worldFolder.GetDirectories("DIM1");
            if (!theEndFolder.Any())
                return false;

            var regionFolder = theEndFolder.First().GetDirectories("region");
            if (!regionFolder.Any())
                return false;

            theEnd.Size = theEndFolder.First().GetSize();
            return true;
        }
    }
}
