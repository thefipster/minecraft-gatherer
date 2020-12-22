using System;
using System.Collections.Generic;
using System.Text;
using TheFipster.Minecraft.Overview.Domain;

namespace TheFipster.Minecraft.Overview.Abstractions
{
    public interface IJobWriter
    {
        void Upsert(RenderJob job);
    }
}
