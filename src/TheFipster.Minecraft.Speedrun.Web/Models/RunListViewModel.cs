using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Web.Models
{
    public class RunListViewModel
    {
        public RunListViewModel()
            => Runs = new List<RunHeaderViewModel>();

        public RunListViewModel(ICollection<RunHeaderViewModel> header)
            => Runs = header;

        public ICollection<RunHeaderViewModel> Runs { get; set; }
    }
}
