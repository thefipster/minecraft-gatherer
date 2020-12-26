using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class RenderQueue : IRenderQueue
    {
        private ICollection<RenderJob> _jobs;

        public RenderQueue()
            => _jobs = new List<RenderJob>();

        public void Enqueue(RenderJob job)
        {
            lock (_jobs)
                _jobs.Add(job);
        }

        public RenderJob Dequeue()
        {
            lock (_jobs)
                return _jobs.OrderByDescending(x => x.Priority).FirstOrDefault();
        }
    }
}
