using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Import.Domain;
using TheFipster.Minecraft.Modules.Abstractions;

namespace TheFipster.Minecraft.Modules.Components
{
    public class EnhanceModule : IEnhanceModule
    {
        private readonly IRunEventConverter _runEventConverter;

        public EnhanceModule(IRunEventConverter runEventConverter)
        {
            _runEventConverter = runEventConverter;
        }

        public RunImport Enhance(RunImport run)
        {
            run.Events = _runEventConverter.Convert(run);

            return run;
        }
    }
}
