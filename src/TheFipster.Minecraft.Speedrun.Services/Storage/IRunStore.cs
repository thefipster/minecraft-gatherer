using System;
using System.Collections.Generic;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public interface IRunStore
    {
        void Add(RunInfo run);

        IEnumerable<RunInfo> Get();

        IEnumerable<RunInfo> Get(DateTime date);

        RunInfo Get(string name);

        bool Exists(string name);

        void Update(RunInfo run);

        int CountValids();
        int Count();

    }
}
