using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Modules.Components
{
    public class MapRenderModule : IMapRenderModule
    {
        private readonly IMapRenderer _mapRenderer;

        public MapRenderModule(IMapRenderer mapRenderer)
        {
            _mapRenderer = mapRenderer;
        }

        public void Render(RenderJob job)
        {
        }
    }
}
