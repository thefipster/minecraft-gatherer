using LiteDB;
using System;

namespace TheFipster.Minecraft.Overview.Domain
{
    public class RenderJob
    {
        public RenderJob()
            => CreatedOn = DateTime.UtcNow;

        public RenderJob(string worldname, bool force = false)
            : this()
        {
            Worldname = worldname;
            Force = force;
        }

        public RenderJob(RenderRequest request)
            : this(request.Worldname, request.Force)
        { }

        [BsonId]
        public string Worldname { get; set; }
        public bool Force { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? RenderStartedOn { get; set; }
    }
}