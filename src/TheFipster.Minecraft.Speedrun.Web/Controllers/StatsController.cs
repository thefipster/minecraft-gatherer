using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheFipster.Minecraft.Speedrun.Web.Controllers
{
    public class StatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}