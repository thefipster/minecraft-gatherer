namespace TheFipster.Minecraft.Overview.Domain
{
    public class RenderTicket
    {
        public string Worldname { get; set; }
        public bool IsEnqueued { get; set; }
        public int Position { get; set; }
        public string Message { get; set; }

        public static RenderTicket CreateSuccess(RenderRequest request, int position)
        {
            return new RenderTicket
            {
                IsEnqueued = true,
                Worldname = request.Worldname,
                Position = position
            };
        }

        public static RenderTicket CreateDuplicate(RenderRequest request, int position)
        {
            return new RenderTicket
            {
                IsEnqueued = false,
                Worldname = request.Worldname,
                Position = position,
                Message = $"The job is already enqueued on position {position}."
            };
        }
    }
}
