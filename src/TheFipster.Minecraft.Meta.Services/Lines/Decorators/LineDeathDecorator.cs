using System.Collections.Generic;
using System.Linq;
using TheFipster.Minecraft.Abstractions;
using TheFipster.Minecraft.Domain;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Lines.Decorators
{
    public class LineDeathDecorator : ILogLineEventConverter
    {
        private readonly ILogLineEventConverter _component;
        private IEnumerable<Player> _players;

        public LineDeathDecorator(ILogLineEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _players = playerStore.GetPlayers();
        }

        public ICollection<RunEvent> Convert(LogLine line)
        {
            var events = _component.Convert(line);
            var player = extractPlayer(line);
            if (player == null)
                return events;

            foreach (var mobCause in EventNames.MobDeathTranslation)
            {
                if (line.Message.ToLower().Contains(mobCause.Key))
                {
                    events.Add(new RunEvent(EventTypes.Death, line.Timestamp, mobCause.Value, player.Id));
                    return events;
                }
            }

            if (line.Message.ToLower().Contains(" killed by ")
                || line.Message.ToLower().Contains(" slain by ")
                || line.Message.ToLower().Contains(" escape "))
            {
                foreach (var storedPlayer in _players)
                {
                    if (!line.Message.Split(" ").Skip(3).Any(x => x.Contains(storedPlayer.Name)))
                        continue;

                    events.Add(new RunEvent(EventTypes.Death, line.Timestamp, storedPlayer.Name, player.Id));
                    return events;
                }
            }

            foreach (var mobCause in EventNames.NaturalDeathTranslation)
            {
                if (line.Message.ToLower().Contains(mobCause.Key))
                {
                    events.Add(new RunEvent(EventTypes.Death, line.Timestamp, mobCause.Value, player.Id));
                    return events;
                }
            }

            return events;
        }

        private Player extractPlayer(LogLine line)
        {
            foreach (var player in _players)
                if (line.Message.Split(" ").Take(3).Any(x => x.Contains(player.Name)))
                    return player;

            return null;
        }
    }
}
