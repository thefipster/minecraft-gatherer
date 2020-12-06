using System.Collections.Generic;

namespace TheFipster.Minecraft.Import.Domain
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
            { Dimensions.Overworld, new DimensionTranslation("Overworld", string.Empty) },
            { Dimensions.Nether, new DimensionTranslation("Nether", "DIM-1") },
            { Dimensions.TheEnd, new DimensionTranslation("The End", "DIM1") }
        };
    }

    public class DimensionTranslation
    {
        public DimensionTranslation(string name, string folder)
        {
            Name = name;
            Folder = folder;
        }

        public string Name { get; set; }
        public string Folder { get; set; }
    }
}
