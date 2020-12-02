namespace TheFipster.Minecraft.Core.Domain
{
    public class Problem
    {
        public Problem()
        {
        }

        public Problem(string message)
            => Message = message;

        public Problem(string message, string exception)
            : this(message)
            => Exception = exception;

        public string Message { get; set; }
        public string Exception { get; set; }

        public override string ToString()
            => $"{Message} - Ex: {Exception}";
    }
}
