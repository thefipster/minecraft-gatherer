using System;
using System.Linq;
using TheFipster.Minecraft.Gatherer.Services;

namespace TheFipster.Minecraft.Gatherer
{
    class Program
    {
        static void Main(string[] args)
        {
            var filesystemConfig = new FilesystemConfig();
            var logArchiveHandler = new LogArchiveHandler(filesystemConfig);
            var lineInterpreter = new LineInterpreter();
            var logInterpreter = new LogInterpreter(logArchiveHandler, lineInterpreter);

            var logs = logArchiveHandler.Find();

            foreach (var log in logs)
            {
                var sessions = logInterpreter.Split(log).ToList();
            }

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
