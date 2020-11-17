using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class RunFinder : IRunFinder
    {
        private readonly IRunStore _runStore;

        public RunFinder(IRunStore runStore)
        {
            _runStore = runStore;
        }

        public IEnumerable<RunInfo> GetAll() => _runStore
            .Get();

        public RunInfo GetByName(string worldName) => _runStore
            .Get(worldName);

        public IEnumerable<RunInfo> GetFinished() => _runStore
            .Get()
            .Where(x => x.Validity.IsValid && x.Outcome.IsFinished);

        public IEnumerable<RunInfo> GetStarted() => _runStore
            .Get()
            .Where(x =>
                x.Validity.IsValid
                && x.Logs.Events.Any(y => y.Type == LogEventTypes.SetTime));

        public IEnumerable<RunInfo> GetValid() => _runStore
            .Get()
            .Where(x => x.Validity.IsValid);
    }
}
