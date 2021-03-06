﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheFipster.Minecraft.Modules;
using TheFipster.Minecraft.Speedrun.Web.Models;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    [Authorize]
    public class ImportController : Controller
    {
        private readonly ISyncModule _syncModule;

        public ImportController(ISyncModule syncModule)
            => _syncModule = syncModule;

        public IActionResult Index()
        {
            var viewmodel = importWorlds();
            return View(viewmodel);
        }

        public IActionResult Force()
        {
            var viewmodel = importWorlds(true);
            return View("Index", viewmodel);
        }

        private ImportIndexViewModel importWorlds(bool force = false)
        {
            var sync = _syncModule.Synchronize(force);

            return new ImportIndexViewModel
            {
                Sync = sync
            };
        }
    }
}