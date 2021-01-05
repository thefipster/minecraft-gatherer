using LiteDB;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class OutcomeWriter : IOutcomeWriter
    {
        private readonly ILiteCollection<RunMeta<Outcomes>> _collection;

        public OutcomeWriter(IMetaDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunMeta<Outcomes>>("MetaOutcomes");

        public void Insert(RunMeta<Outcomes> meta)
            => _collection.Insert(meta);

        public void Update(RunMeta<Outcomes> meta)
            => _collection.Update(meta);

        public void Upsert(RunMeta<Outcomes> meta)
        {
            if (Exists(meta.Worldname))
                Update(meta);
            else
                Insert(meta);
        }

        public bool Exists(string worldname)
            => _collection.Exists(x => x.Worldname == worldname);
    }
}
