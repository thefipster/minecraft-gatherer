using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Speedrun.Web.Converter;
using TheFipster.Minecraft.Speedrun.Web.Models;
using TheFipster.Minecraft.Storage.Abstractions;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class RunsController : Controller
    {
        private readonly IRunFinder _runFinder;
        private readonly IRunListConverter _listConverter;

        public RunsController(
            IRunFinder runFinder,
            IRunListConverter listConverter)
        {
            _runFinder = runFinder;
            _listConverter = listConverter;
        }

        public IActionResult Index()
        {
            var runs = _runFinder.GetFinished();
            var header = _listConverter.Convert(runs);
            var viewmodel = new RunListViewModel(header);
            return View(viewmodel);
        }

        public IActionResult Started()
        {
            var runs = _runFinder.GetStarted();
            var header = _listConverter.Convert(runs);
            var viewmodel = new RunListViewModel(header);
            return View(viewmodel);
        }

        public IActionResult All()
        {
            var runs = _runFinder.GetValid();
            var header = _listConverter.Convert(runs);
            var viewmodel = new RunListViewModel(header);
            return View(viewmodel);
        }

        public IActionResult Garbage()
        {
            var runs = _runFinder.GetAll();
            var header = _listConverter.Convert(runs);
            var viewmodel = new RunListViewModel(header);
            return View(viewmodel);
        }
    }
}