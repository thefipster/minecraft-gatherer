using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class JobReader : IJobReader
    {
        private readonly ILiteCollection<RenderJob> _collection;

        public JobReader(IOverviewerDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RenderJob>();

        public IEnumerable<RenderJob> Get()
            => _collection.FindAll();

        public RenderJob Get(string worldname)
            => _collection.FindOne(x => x.Worldname == worldname);

        public int Count()
            => _collection.Count();

        public bool Exists(string worldname)
            => _collection.Exists(x => x.Worldname == worldname);
    }
}
