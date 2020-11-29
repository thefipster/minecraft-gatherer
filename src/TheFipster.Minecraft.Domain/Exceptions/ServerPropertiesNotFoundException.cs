using System;

namespace TheFipster.Minecraft.Domain.Exceptions
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
