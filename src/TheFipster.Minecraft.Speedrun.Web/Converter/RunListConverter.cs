using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Speedrun.Web.Models;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Converter
{
    public class RunListConverter : IRunListConverter
    {
        private readonly IManualsStore _manualsStore;

        public RunListConverter(IManualsStore manualsStore)
        {
            _manualsStore = manualsStore;
        }

        public ICollection<RunHeaderViewModel> Convert(IEnumerable<RunAnalytics> analytics)
        {
            var manuals = _manualsStore.Get();

            var list = new List<RunHeaderViewModel>();
            foreach (var run in analytics)
            {
                var manual = manuals.FirstOrDefault(x => x.Worldname == run.Worldname);
                var runModel = new RunHeaderViewModel(run, manual);
                list.Add(runModel);
            }

            return list;
        }
    }
}
