using System;

namespace TheFipster.Minecraft.Gatherer.Exceptions
{
    public class InvalidLineCollectionStateException : Exception
    {
        public InvalidLineCollectionStateException()
        {
        }

        public InvalidLineCollectionStateException(string message)
            : base(message)
        {
        }

        public InvalidLineCollectionStateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
