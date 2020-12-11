using System.Collections.Generic;

namespace TheFipster.Minecraft.Core.Domain
{
    public enum Dimensions
    {
        Overworld,
        Nether,
        TheEnd
    }

    public static class DimensionTranslations
    {
        public static Dictionary<Dimensions, DimensionTranslation> Items => new Dictionary<Dimensions, DimensionTranslation>
        {
            { Dimensions.Overworld, new DimensionTranslation("minecraft:overworld", "Overworld", string.Empty) },
            { Dimensions.Nether, new DimensionTranslation("minecraft:nether", "Nether", "DIM-1") },
            { Dimensions.TheEnd, new DimensionTranslation("minecraft:end", "The End", "DIM1") }
        };
    }

    public class DimensionTranslation
    {
        public DimensionTranslation(string id, string name, string folder)
        {
            Id = id;
            Name = name;
            Folder = folder;
        }

        public string Name { get; set; }
        public string Folder { get; set; }

        public string Id { get; set; }
    }
}
