using System.Collections.Generic;

namespace TheFipster.Minecraft.Extender.Domain
{
    public class DayContent
    {
        public DayContent()
        {
            Items = new List<string>();
        }

        public ICollection<string> Items { get; set; }
    }
}
