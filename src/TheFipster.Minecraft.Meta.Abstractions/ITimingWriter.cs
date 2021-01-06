using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface ITimingWriter
    {
        void Upsert(RunMeta<int> meta);

        bool Exists(RunMeta<int> meta);

        bool Exists(string worldname, MetaFeatures feature);
    }
}
