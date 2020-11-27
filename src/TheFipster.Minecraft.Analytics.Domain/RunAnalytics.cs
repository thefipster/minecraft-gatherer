using LiteDB;

namespace TheFipster.Minecraft.Analytics.Domain
{
    public class RunAnalytics
    {
        public RunAnalytics() { }

        public RunAnalytics(string worldname)
            => Worldname = worldname;

        [BsonId]
        public string Worldname { get; set; }
        public TimingAnalytics Timings { get; set; }
    }
}
