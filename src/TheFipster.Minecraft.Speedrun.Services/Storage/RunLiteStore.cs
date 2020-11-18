using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using TheFipster.Minecraft.Speedrun.Domain;

namespace TheFipster.Minecraft.Speedrun.Services
{
    public class RunLiteStore : IRunStore
    {
        private const string DatabaseFilename = "run.litedb";

        private ILiteCollection<RunInfo> _collection;

        public RunLiteStore(IConfigService config)
        {
            var dbPath = Path.Combine(config.DataLocation.FullName, DatabaseFilename);
            var db = new LiteDatabase(dbPath);
            _collection = db.GetCollection<RunInfo>();
        }

        public void Add(RunInfo run)
            => _collection.Insert(run);

        public bool Exists(string name)
            => _collection.Exists(x => x.Id == name);

        public IEnumerable<RunInfo> Get()
            => _collection.FindAll();

        public IEnumerable<RunInfo> Get(DateTime date)
            => _collection.Find(x => x.World.CreatedOn.Date == date.Date);

        public RunInfo Get(string name)
            => _collection.FindOne(x => x.Id == name);

        public void Update(RunInfo run)
            => _collection.Update(run);

        public int CountValids()
            => _collection.Count(x => x.Validity.IsValid);

        public int Count()
            => _collection.Count();
    }
}
