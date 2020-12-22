using LiteDB;
using TheFipster.Minecraft.Meta.Abstractions;
using TheFipster.Minecraft.Meta.Domain;

namespace TheFipster.Minecraft.Meta.Services
{
    public class TimingWriter : ITimingWriter
    {
        private readonly ILiteCollection<RunMeta<int>> _collection;

        public TimingWriter(IMetaDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RunMeta<int>>("MetaTiming");

        public void Insert(RunMeta<int> meta)
            => _collection.Insert(meta);

        public void Update(RunMeta<int> meta)
            => _collection.Update(meta);

        public void Upsert(RunMeta<int> meta)
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
