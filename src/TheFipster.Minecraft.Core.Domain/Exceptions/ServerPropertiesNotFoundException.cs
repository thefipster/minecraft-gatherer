using System;

namespace TheFipster.Minecraft.Core.Domain.Exceptions
{
    public class ServerPropertiesNotFoundException : Exception
    {
        public ServerPropertiesNotFoundException()
        {

        }

        public ServerPropertiesNotFoundException(string message) : base(message)
        {

        }
    }
}
