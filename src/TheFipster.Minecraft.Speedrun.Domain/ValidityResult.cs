using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class ValidityResult
    {
        public ValidityResult()
        {
            IsValid = true;
            Reasons = new List<string>();
        }
        public bool IsValid { get; set; }
        public List<string> Reasons { get; set; }
    }
}
