using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface IRuntimeWriter
    {
        void Insert(RunMeta<int> meta);

        void Update(RunMeta<int> meta);

        void Upsert(RunMeta<int> meta);

        bool Exists(string worldname);
    }
}
