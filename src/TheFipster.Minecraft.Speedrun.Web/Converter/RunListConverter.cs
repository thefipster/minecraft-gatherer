using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Analytics.Domain;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Converter
{
    public class RunListConverter : IRunListConverter
    {
        private readonly IManualsReader _manualsReader;

        public RunListConverter(IManualsReader manualsReader)
        {
            _manualsReader = manualsReader;
        }

        public ICollection<RunHeaderViewModel> Convert(IEnumerable<RunAnalytics> analytics)
        {
            var manuals = _manualsReader.Get();

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
