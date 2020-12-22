using System.Linq;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class RenderAssigner
    {
        private readonly IJobWriter _writer;
        private readonly IJobReader _reader;
        private readonly IJobPrioritizer _prioritizer;

        public RenderAssigner(
            IJobWriter writer,
            IJobReader reader,
            IJobPrioritizer prioritizer)
        {
            _writer = writer;
            _reader = reader;
            _prioritizer = prioritizer;
        }

        public RenderTicket Enqueue(RenderRequest request)
        {
            var job = new RenderJob(request);
            job.Priority = _prioritizer.Prioritize(request.Worldname);

            var position = _reader.Get().Count(x => x.Priority > job.Priority);

            if (request.Force
                || !_reader.Exists(request.Worldname))
            {
                _writer.Upsert(job);
                return RenderTicket.CreateSuccess(request, position);
            }

            job = _reader.Get(request.Worldname);
            position = _reader.Get().Count(x => x.Priority > job.Priority);
            return RenderTicket.CreateDuplicate(request, position);
        }
    }
}
