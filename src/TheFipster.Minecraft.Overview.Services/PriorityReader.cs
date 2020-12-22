using LiteDB;
using System;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class PriorityReader
    {
        private readonly ILiteCollection<RenderJob> _jobCollection;
        private readonly ILiteCollection<RenderResult> _resultCollection;

        public PriorityReader(IOverviewerDatabaseHandler databaseHandler)
        {
            _jobCollection = databaseHandler.GetCollection<RenderJob>();
        }

        public RenderJob Next()
            => _jobCollection
                .Query()
                .OrderByDescending(x => x.Priority)
                .Limit(1)
                .FirstOrDefault();

        public void Start(string worldname)
        {
            var job = _jobCollection.FindOne(x => x.Worldname == worldname);
            job.RenderStartedOn = DateTime.UtcNow;
            _jobCollection.Update(job);
        }

        public void Finish(RenderResult result)
        {
            _resultCollection.Insert(result);
            _jobCollection.Delete(new BsonValue(result.Worldname));
        }
    }
}
