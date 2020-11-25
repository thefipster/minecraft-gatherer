using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Import.Services.Extensions;
using TheFipster.Minecraft.Services.Abstractions;

namespace TheFipster.Minecraft.Import.Services
{
    public class DimensionLoader : IDimensionLoader
    {
        public ICollection<DimensionInfo> Load(WorldInfo world)
        {
            var dimensions = new List<DimensionInfo>();
            var worldFolder = new DirectoryInfo(world.Path);

            if (tryLoadOverworld(worldFolder, out var overworld))
                dimensions.Add(overworld);

            if (tryLoadDimension(worldFolder, "Nether", "DIM-1", out var nether))
                dimensions.Add(nether);

            if (tryLoadDimension(worldFolder, "The End", "DIM1", out var theend))
                dimensions.Add(theend);

            return dimensions;
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

        private bool tryLoadDimension(DirectoryInfo worldFolder, string dimensionName, string dimensionFolder, out DimensionInfo dimension)
        {
            dimension = new DimensionInfo(dimensionName);

            var folder = worldFolder.GetDirectories(dimensionFolder);
            if (!folder.Any())
                return false;

            var regionFolder = folder.First().GetDirectories("region");
            if (!regionFolder.Any())
                return false;

            dimension.Size = folder.First().GetSize();
            return true;
        }
    }
}
