using SimpleInjector;
using TheFipster.Minecraft.Speedrun.Web.Converter;

namespace TheFipster.Minecraft.Speedrun.Web.Dependencies
{
    public static class WebDeps
    {
        public static void RegisterWeb(this Container container)
        {
            container.Register<IRunListConverter, RunListConverter>();
        }
    }
}
