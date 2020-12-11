using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Import.Services.Extensions;

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

            if (tryLoadDimension(worldFolder, Dimensions.Nether, out var nether))
                dimensions.Add(nether);

            if (tryLoadDimension(worldFolder, Dimensions.TheEnd, out var theEnd))
                dimensions.Add(theEnd);

            return dimensions;
        }

        private bool tryLoadOverworld(DirectoryInfo worldFolder, out DimensionInfo overworld)
        {
            overworld = new DimensionInfo(Dimensions.Overworld);

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

        private bool tryLoadDimension(DirectoryInfo worldFolder, Dimensions dimensionType, out DimensionInfo dimension)
        {
            dimension = new DimensionInfo(dimensionType);

            var folderName = DimensionTranslations.Items[dimensionType].Folder;
            var folder = worldFolder.GetDirectories(folderName);
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
