using LiteDB;
using System;

namespace TheFipster.Minecraft.Overview.Domain
{
    public class RenderResult
    {
        public RenderResult()
        {
            Id = Guid.NewGuid();
        }

        public RenderResult(string worldname) : this()
        {
            Worldname = worldname;
        }

        [BsonId]
        public Guid Id { get; set; }
        public string Worldname { get; set; }
        public string StdOut { get; set; }
        public bool Success { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime EndedOn { get; set; }
        public string Message { get; set; }
    }
}
