using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IRenderStateReader
    {
        RenderState Get(string worldname);
    }
}
