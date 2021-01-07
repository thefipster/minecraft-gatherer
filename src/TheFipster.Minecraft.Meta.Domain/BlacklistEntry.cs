using LiteDB;

namespace TheFipster.Minecraft.Meta.Domain
{
    public class BlacklistEntry
    {
        public BlacklistEntry() { }

        public BlacklistEntry(string worldname)
            => Worldname = worldname;

        [BsonId]
        public string Worldname { get; set; }
    }
}
