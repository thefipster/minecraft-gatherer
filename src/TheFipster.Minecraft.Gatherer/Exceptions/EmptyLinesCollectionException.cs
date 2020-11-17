using System;

namespace TheFipster.Minecraft.Gatherer.Exceptions
{
    public class EmptyLinesCollectionException : Exception
    {
        public EmptyLinesCollectionException()
        {
        }

        public EmptyLinesCollectionException(string message)
            : base(message)
        {
        }

        public EmptyLinesCollectionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
