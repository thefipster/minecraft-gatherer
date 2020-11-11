using System;
using System.Linq;
using TheFipster.Minecraft.Gatherer.Analyzers;
using TheFipster.Minecraft.Gatherer.Services;

namespace TheFipster.Minecraft.Gatherer
{
    class Program
    {
        static void Main(string[] args)
        {
            var filesystemConfig = new FilesystemConfig();
            var filesystemTools = new FilesystemTools();
            var logArchiveHandler = new LogArchiveHandler(filesystemConfig);
            var lineInterpreter = new LineInterpreter();
            var logInterpreter = new LogInterpreter(logArchiveHandler);
            var logSessionHandler = new LogSessionHandler(filesystemConfig, filesystemTools);
            var logAnalyzer = new LogAnalyzer(
                new TimeAnalyzer()
            );

            var logs = logArchiveHandler.Find();

            foreach (var log in logs)
            {
                var splits = logInterpreter.Split(log).ToList();

                foreach (var split in splits)
                {
                    var session = lineInterpreter.Read(split);
                    var sessionPath = logSessionHandler.Write(session);
                }
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
