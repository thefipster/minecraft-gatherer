using System.Linq;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Services
{
    public class RenderStateReader : IRenderStateReader
    {
        private readonly IRenderQueue _queue;
        private readonly IResultReader _reader;

        public RenderStateReader(IRenderQueue queue, IResultReader reader)
        {
            _queue = queue;
            _reader = reader;
        }

        public RenderState Get(string worldname)
        {
            var result = _reader.Get(worldname);
            if (result != null && result.Success)
                return RenderState.Complete;

            if (_queue.Active != null && _queue.Active.Worldname == worldname)
                return RenderState.Active;

            var queueItem = _queue.PeakAll().FirstOrDefault(x => x.Worldname == worldname);
            if (queueItem != null)
            {
                var length = _queue.PeakAll().Count();
                var position = _queue.PeakAll().Count(x => x.Priority <= queueItem.Priority);

                return RenderState.InQueue(position, length);
            }

            return RenderState.None;
        }
    }
}
