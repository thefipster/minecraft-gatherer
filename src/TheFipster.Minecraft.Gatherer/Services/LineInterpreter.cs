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
            private DateTime logDate;

            public LogSession Read(LogArchive log)
            {
                logDate = log.Date;
                var logLines = new List<LogLine>();
                var templines = new List<string>();

                foreach (var line in log.Lines)
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

                var lastLine = readLine(templines);
                logLines.Add(lastLine);

                return new LogSession
                {
                    Lines = logLines,
                    OriginalArchive = log.Path,
                    Start = logLines.Min(x => x.Timestamp),
                    End = logLines.Max(x => x.Timestamp)
                };
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

                time = sanitizeBraces(time);
                var timespan = TimeSpan.Parse(time);
                var timestamp = logDate.Add(timespan);

                threadAndLevel = sanitizeBraces(threadAndLevel);
                var splitIndex = threadAndLevel.LastIndexOf("/");
                var thread = threadAndLevel.Substring(0, splitIndex);
                var level = threadAndLevel.Substring(splitIndex + 1);

                var messageIndex = lineHead.IndexOf("]: ");
                var message = string.Concat(lineHead.Substring(messageIndex + 3), tail);

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
