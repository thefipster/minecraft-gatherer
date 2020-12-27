using LiteDB;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class ResultWriter : IResultWriter
    {
        private readonly ILiteCollection<RenderResult> _collection;

        public ResultWriter(IOverviewerDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RenderResult>();

        public void Upsert(RenderResult result)
        {
            if (_collection.Exists(x => x.Worldname == result.Worldname))
                _collection.Update(result);
            else
                _collection.Insert(result);
        }
    }
}
