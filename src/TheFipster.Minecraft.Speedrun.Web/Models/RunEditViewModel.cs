using System;
using TheFipster.Minecraft.Manual.Domain;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class RunEditViewModel
    {
        public RunEditViewModel() { }

        public RunEditViewModel(RunManuals manuals)
        {
            Worldname = manuals.Worldname;
            YoutubeLink = manuals.YoutubeLink;
            SpeedrunLink = manuals.SpeedrunLink;

            if (manuals.RuntimeInMs.HasValue)
                Runtime = TimeSpan
                    .FromMilliseconds(manuals.RuntimeInMs.Value)
                    .ToString(@"hh\:mm\:ss\:fff");
        }

        public string Worldname { get; set; }
        public string YoutubeLink { get; set; }
        public string SpeedrunLink { get; set; }
        public string Runtime { get; set; }
    }
}