using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Modules.Abstractions;
using TheFipster.Minecraft.Overview.Abstractions;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Modules.Components
{
    public class MapRenderModule : IMapRenderModule
    {
        private const string OverviewerFolder = "overviewer";

        private readonly IRenderQueue _queue;
        private readonly IMapRenderer _renderer;
        private readonly IJobPrioritizer _prioritizer;
        private readonly IResultWriter _writer;
        private readonly IResultReader _reader;
        private readonly IConfigService _config;
        private readonly IWorldFinder _finder;
        private readonly IWorldArchivist _archivist;

        public MapRenderModule(IRenderQueue queue, IMapRenderer renderer, IJobPrioritizer prioritizer, IResultWriter writer, IResultReader reader, IConfigService config, IWorldFinder finder, IWorldArchivist archivist)
        {
            _queue = queue;
            _renderer = renderer;
            _prioritizer = prioritizer;
            _writer = writer;
            _reader = reader;
            _config = config;
            _finder = finder;
            _archivist = archivist;
        }

        public void CreateJob(string worldname)
        {
            var priority = _prioritizer.Prioritize(worldname);
            var job = new RenderJob(worldname);
            job.Priority = priority;
            _queue.Enqueue(job);
        }

        public IEnumerable<RenderJob> GetJobs()
            => _queue.PeakAll();

        public IEnumerable<RenderResult> GetResults()
            => _reader.Get();

        public void Render(RenderJob job)
        {
            var input = locateWorld(job.Worldname);
            var output = locateWebpath(job.Worldname);

            var result = _renderer.Render(job.Worldname, input, output);
            _writer.Upsert(result);
        }

        private string locateWorld(string worldname)
        {
            var worldLocation = _finder.Find(worldname);

            if (worldLocation is FileInfo)
                worldLocation = _archivist.Decompress(worldname);

            return worldLocation.FullName;
        }

        private string locateWebpath(string worldname)
            => Path.Combine(
                _config.DataLocation.FullName,
                OverviewerFolder,
                worldname);
    }
}
