using SimpleInjector;
using TheFipster.Minecraft.Manual.Abstractions;
using TheFipster.Minecraft.Manual.Services;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class ManualDeps
    {
        public static void RegisterManuals(this Container container)
        {
            container.Register<IManualsWriter, ManualsWriter>(Lifestyle.Scoped);
            container.Register<IManualsReader, ManualsReader>(Lifestyle.Scoped);
        }
    }
}
