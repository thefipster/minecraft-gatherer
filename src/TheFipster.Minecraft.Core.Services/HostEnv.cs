using System;
using TheFipster.Minecraft.Core.Abstractions;

namespace TheFipster.Minecraft.Core.Services
{
    public class HostEnv : IHostEnv
    {
        public bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
    }
}
