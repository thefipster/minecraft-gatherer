using LiteDB;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class JobWriter : IJobWriter
    {
        private readonly ILiteCollection<RenderJob> _collection;

        public JobWriter(IOverviewerDatabaseHandler databaseHandler)
            => _collection = databaseHandler.GetCollection<RenderJob>();

        public void Upsert(RenderJob job)
        {
            if (_collection.Exists(x => x.Worldname == job.Worldname))
                _collection.Update(job);
            else
                _collection.Insert(job);
        }
    }
}
