using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Manual.Abstractions
{
    public interface IManualsWriter
    {
        void Upsert(RunManuals manuals);
        bool Exists(string name);
    }
}
