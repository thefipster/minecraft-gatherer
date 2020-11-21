using System;

namespace TheFipster.Minecraft.Speedrun.Domain
{
    public class RunInput
    {
        public string Id { get; set; }

        public string YoutubeLink { get; set; }
        public string SpeedrunLink { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsVerified { get; set; }
        public bool IsChecked { get; set; }
    }
}
