using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IMapRenderer
    {
        RenderResult Render(string worldname);
    }
}
