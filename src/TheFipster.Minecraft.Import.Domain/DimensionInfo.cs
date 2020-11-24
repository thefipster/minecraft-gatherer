namespace TheFipster.Minecraft.Import.Domain
{
    public class DimensionInfo
    {
        public DimensionInfo()
        {

        }

        public DimensionInfo(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public long Size { get; set; }
    }
}
