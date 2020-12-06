using LiteDB;
using System;

namespace TheFipster.Minecraft.Meta.Domain
{
    public class RunMeta<T>
    {
        public RunMeta()
        {

        }

        public RunMeta(string worldname, DateTime timestamp, MetaFeatures feature, T value)
        {
            Worldname = worldname;
            Timestamp = timestamp;
            Feature = feature;
            Value = value;
        }

        [BsonId]
        public string Worldname { get; set; }
        public DateTime Timestamp { get; set; }
        public MetaFeatures Feature { get; set; }
        public T Value { get; set; }
    }
}
