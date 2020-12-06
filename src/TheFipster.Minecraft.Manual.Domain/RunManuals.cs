using LiteDB;

namespace TheFipster.Minecraft.Manual.Domain
{
    public class RunManuals
    {
        public RunManuals() { }

        public RunManuals(string worldname)
            => Worldname = worldname;

        [BsonId]
        public string Worldname { get; set; }

        public string YoutubeLink { get; set; }
        public string SpeedrunLink { get; set; }
        public long? RuntimeInMs { get; set; }
    }
}
