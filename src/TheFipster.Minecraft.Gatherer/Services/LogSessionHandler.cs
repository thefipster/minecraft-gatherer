using Newtonsoft.Json;
using System.IO;
using TheFipster.Minecraft.Gatherer.Models;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class LogSessionHandler
    {
        private readonly FilesystemConfig config;
        private readonly FilesystemTools tools;

        public LogSessionHandler(FilesystemConfig fsConfig, FilesystemTools fsTools)
        {
            this.config = fsConfig;
            this.tools = fsTools;
        }

        public string Write(LogSession session)
        {
            var filepath = getFilepath(session);
            tools.EnsureFilepathExists(filepath);
            writeJson(session, filepath);
            return filepath;
        }

        private string getFilepath(LogSession session)
        {
            var filename = session.Start.ToString("yyyy-MM-dd HH-mm-ss");
            var filepath = Path.Combine(config.DataPath, "logs", filename + ".log");
            return filepath;
        }

        private static void writeJson(LogSession session, string filepath)
        {
            var json = JsonConvert.SerializeObject(session, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }
    }
}
