using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Modules.Abstractions
{
    public interface IMapRenderModule
    {
        void Render(RenderJob job);
    }
}
