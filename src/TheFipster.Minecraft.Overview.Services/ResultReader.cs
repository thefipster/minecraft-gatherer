using LiteDB;
using System.Collections.Generic;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class ResultReader : IResultReader
    {
        private readonly ILiteCollection<RenderResult> _collection;

        public ResultReader(IOverviewerDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RenderResult>();

        public IEnumerable<RenderResult> Get()
            => _collection.FindAll();

        public RenderResult Get(string worldname)
            => _collection.FindOne(x => x.Worldname == worldname);

        public int Count()
            => _collection.Count();

        public bool Exists(string worldname)
            => _collection.Exists(x => x.Worldname == worldname);
    }
}
