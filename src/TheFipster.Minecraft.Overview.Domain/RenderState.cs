namespace TheFipster.Minecraft.Overview.Domain
{
    public class RenderState
    {
        public bool IsCompleted { get; set; }
        public int Position { get; set; }
        public int QueueLength { get; set; }
        public bool IsActive { get; set; }

        public static RenderState None => new RenderState();

        public static RenderState Complete => new RenderState
        {
            IsCompleted = true
        };

        public static RenderState Active => new RenderState
        {
            IsActive = true
        };

        public static RenderState InQueue(int position, int length) => new RenderState
        {
            Position = position,
            QueueLength = length
        };
    }
}
