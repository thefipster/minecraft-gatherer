using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IRenderQueue
    {
        void Enqueue(RenderJob job);
        RenderJob Dequeue();
    }
}
