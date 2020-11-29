using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TheFipster.Minecraft.Import.Abstractions;
using TheFipster.Minecraft.Import.Domain;

namespace TheFipster.Minecraft.Import.Services
{
    public class LogParser : ILogParser
    {
        private DateTime logDate;

        public IEnumerable<LogLine> Read(IEnumerable<string> entries, DateTime date)
        {
            logDate = date.Date;
            var logLines = new List<LogLine>();
            var templines = new List<string>();

            foreach (var line in entries.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                if (IsNewLine(line))
                {
                    if (templines.Any())
                    {
                        var parsedLine = readLine(templines);
                        logLines.Add(parsedLine);
                        templines.Clear();
                    }

                    templines.Add(line);
                }
            }

            if (templines.Any())
            {
                var lastLine = readLine(templines);
                logLines.Add(lastLine);
            }

            return logLines;
        }

        private LogLine readLine(IEnumerable<string> lines)
        {
            guardRead(lines);

            var lineHead = lines.First();
            var tail = lines.Count() > 1 ? string.Join(Environment.NewLine, lines.Skip(1)) : string.Empty;

            var regEx = new Regex(@"\[(.*?)\]");
            var matches = regEx.Matches(lineHead);

            var time = matches.Skip(0).First().Value;
            var threadAndLevel = matches.Skip(1).First().Value;

            var timestamp = DateTime.MinValue;
            time = sanitizeBraces(time);
            if (TimeSpan.TryParse(time, out var timespan))
                timestamp = logDate.Add(timespan);


            threadAndLevel = sanitizeBraces(threadAndLevel);
            var splitIndex = threadAndLevel.LastIndexOf("/");
            var thread = string.Empty;
            var level = string.Empty;
            if (splitIndex != -1)
            {
                thread = threadAndLevel.Substring(0, splitIndex);
                level = threadAndLevel.Substring(splitIndex + 1);
            }

            var messageIndex = lineHead.IndexOf("]: ");
            var message = string.Concat(lineHead.Substring(messageIndex + 3), tail);

            return new LogLine
            {
                Level = level,
                Message = message,
                Thread = thread,
                Timestamp = timestamp,
                Raw = string.Join(Environment.NewLine, lines)
            };
        }

        private static string sanitizeBraces(string text) => text.Replace("[", string.Empty).Replace("]", string.Empty);

        public bool IsNewLine(string line) => line.First() == '[';

        private void guardRead(IEnumerable<string> lines)
        {
            if (lines.Count() == 0)
                throw new Exception("No lines where given.");

            if (lines.Count() == 1 && !IsNewLine(lines.First()))
                throw new Exception("Single line is not a new line.");

            if (lines.Count() > 1)
            {
                if (!IsNewLine(lines.First()))
                    throw new Exception("First line of the collection is not a new line.");

                if (lines.Skip(1).Any(x => IsNewLine(x) == true))
                    throw new Exception("Follow up lines contain at least one new line.");
            }
        }
    }
}
