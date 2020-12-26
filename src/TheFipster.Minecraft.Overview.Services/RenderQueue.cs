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

        public IEnumerable<RenderJob> PeakAll()
            => _jobs.ToList();

        public void Enqueue(RenderJob job)
        {
            lock (_jobs)
            {
                var duplicate = _jobs.FirstOrDefault(x => x.Worldname == job.Worldname);
                if (duplicate != null)
                {
                    if (duplicate.Priority < job.Priority)
                    {
                        _jobs.Remove(duplicate);
                        _jobs.Add(job);
                    }
                }
                else
                {
                    _jobs.Add(job);
                }
            }
        }

        public void Remove(string worldname)
        {
            lock (_jobs)
            {
                var job = _jobs.FirstOrDefault(x => x.Worldname == worldname);
                if (job != null)
                    _jobs.Remove(job);
            }
        }

        public RenderJob Peak()
        {
            lock (_jobs)
            {
                return _jobs.OrderByDescending(x => x.Priority).FirstOrDefault();
            }
        }
    }
}
