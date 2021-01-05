using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Abstractions
{
    public interface IOutcomeWriter
    {
        void Insert(RunMeta<Outcomes> meta);

        void Update(RunMeta<Outcomes> meta);

        void Upsert(RunMeta<Outcomes> meta);

        bool Exists(string worldname);
    }
}
