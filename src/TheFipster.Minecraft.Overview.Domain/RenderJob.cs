using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Overview.Domain
{
    public class RenderJob
    {
        public string Worldname { get; set; }
        public Dimensions Dimension { get; set; }
        public bool Force { get; set; }
        public int Priority { get; set; }
    }
}
