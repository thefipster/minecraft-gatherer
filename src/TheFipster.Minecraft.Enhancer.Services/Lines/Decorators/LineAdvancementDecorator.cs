﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TheFipster.Minecraft.Core.Abstractions;
using TheFipster.Minecraft.Core.Domain;
using TheFipster.Minecraft.Enhancer.Abstractions;
using TheFipster.Minecraft.Enhancer.Domain;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Enhancer.Services.Lines.Decorators
{
    public class LineAdvancementDecorator : ILogLineEventConverter
    {
        private readonly ILogLineEventConverter _component;
        private readonly IEnumerable<IPlayer> _players;

        public LineAdvancementDecorator(ILogLineEventConverter component, IPlayerStore playerStore)
        {
            _component = component;
            _players = playerStore.GetPlayers();
        }

        public ICollection<RunEvent> Convert(LogLine line)
        {
            var events = _component.Convert(line);

            if (!line.Message.Contains("has made the advancement"))
                return events;

            if (!findPlayer(line, out var player))
                return events;

            if (!findAdvancement(line, out var advancementName))
                return events;

            events.Add(new RunEvent(
                EventTypes.Advancement,
                line.Timestamp,
                advancementName,
                player.Id));

            return events;
        }

        private bool findAdvancement(LogLine line, out string value)
        {
            var regEx = new Regex(@"\[(.*?)\]");
            var matches = regEx.Matches(line.Message);

            if (matches.Any())
            {
                value = matches
                    .First()
                    .Value
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                return true;
            }

            value = null;
            return false;
        }

        private bool findPlayer(LogLine line, out IPlayer player)
        {
            foreach (var p in _players)
            {
                if (line.Message.Contains(p.Name))
                {
                    player = p;
                    return true;
                }
            }

            player = null;
            return false;
        }
    }
}
