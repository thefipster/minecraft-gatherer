using System;

namespace TheFipster.Minecraft.Core.Domain.Exceptions
{
    public class OmittingWorldException : Exception
    {
        public OmittingWorldException()
        {

        }

        public OmittingWorldException(string message) : base(message)
        {

        }
    }
}
