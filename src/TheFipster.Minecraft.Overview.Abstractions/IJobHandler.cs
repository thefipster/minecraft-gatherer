using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IJobHandler
    {
        RenderJob Next();
        void Start(string worldname);
        void Finish(RenderResult result);
    }
}
