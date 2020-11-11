using System;
using System.Collections.Generic;

namespace TheFipster.Minecraft.Gatherer.Models
{
    public class LogArchive
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Lines { get; set; }
    }
}
