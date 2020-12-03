using System.Collections.Generic;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Decorators
{
    public class RunEventAdvancementDecorator : IRunEventConverter
    {
        private readonly IRunEventConverter _component;

        public RunEventAdvancementDecorator(IRunEventConverter component)
        {
            _component = component;
        }

        public IList<RunEvent> Convert(RunImport run)
        {
            var events = _component.Convert(run);

            foreach (var playerAchievements in run.Achievements)
            {
                var playerId = playerAchievements.Key;
                foreach (var achievement in playerAchievements.Value)
                {
                    if (EventNames.AdvancementTranslation.ContainsKey(achievement.Event))
                    {
                        var value = EventNames.AdvancementTranslation[achievement.Event];
                        var runEvent = new RunEvent(
                                EventTypes.Advancement,
                                achievement.Timestamp,
                                value,
                                playerId);

                        if (!events.Contains(runEvent))
                            events.Add(runEvent);
                    }
                }
            }

            return events;
        }
    }
}
