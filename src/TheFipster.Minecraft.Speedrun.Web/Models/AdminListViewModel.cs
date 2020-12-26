using System.Collections.Generic;

namespace TheFipster.Minecraft.Speedrun.Web
{
    public class AdminListViewModel
    {
        public AdminListViewModel()
            => Runs = new List<RunAdminViewModel>();

        public ICollection<RunAdminViewModel> Runs { get; internal set; }
    }
}