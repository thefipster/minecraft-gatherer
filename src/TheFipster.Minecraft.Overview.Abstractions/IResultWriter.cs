using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IResultWriter
    {
        void Upsert(RenderResult result);
    }
}
