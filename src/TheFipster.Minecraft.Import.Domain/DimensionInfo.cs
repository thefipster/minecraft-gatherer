namespace TheFipster.Minecraft.Import.Domain
{
    public class DimensionInfo
    {
        public DimensionInfo() { }

        public DimensionInfo(Dimensions type)
        {
            Type = type;
            Name = DimensionTranslations.Items[type].Name;
        }

        public string Name { get; set; }

        public Dimensions Type { get; set; }

        public long Size { get; set; }

        public override string ToString()
            => Name;
    }
}
