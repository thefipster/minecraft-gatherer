using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TheFipster.Minecraft.Gatherer.Exceptions;
using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class LineInterpreter
    {
        public LogLine Read(string line, DateTime date)
        {
            guardRead(new[] { line });

            var regEx = new Regex(@"\[(.*?)\]");
            var matches = regEx.Matches(line);

            var time = matches.Skip(0).First().Value;
            var threadAndLevel = matches.Skip(1).First().Value;

            time = sanitizeBraces(time);
            var timespan = TimeSpan.Parse(time);
            var timestamp = date.Add(timespan);

            threadAndLevel = sanitizeBraces(threadAndLevel);
            var splitIndex = threadAndLevel.LastIndexOf("/");
            var thread = threadAndLevel.Substring(0, splitIndex);
            var level = threadAndLevel.Substring(splitIndex + 1);

            var messageIndex = line.IndexOf("]: ");
            var message = line.Substring(messageIndex + 3);

            return new LogLine
            {
                Level = level,
                Message = message,
                Thread = thread,
                Timestamp = timestamp
            };
        }

        private static string sanitizeBraces(string text) => text.Replace("[", string.Empty).Replace("]", string.Empty);

        public bool IsNewLine(string line) => line.First() == '[';

        private void guardRead(IEnumerable<string> lines)
        {
            if (lines.Count() == 0)
                throw new EmptyLinesCollectionException("No lines where given.");

            if (lines.Count() == 1 && !IsNewLine(lines.First()))
                throw new InvalidLineCollectionStateException("Single line is not a new line.");

            if (lines.Count() > 1)
            {
                if (!IsNewLine(lines.First()))
                    throw new InvalidLineCollectionStateException("First line of the collection is not a new line.");

                if (lines.Skip(1).Any(x => IsNewLine(x) == true))
                    throw new InvalidLineCollectionStateException("Follow up lines contain at least one new line.");
            }
        }
    }
}
