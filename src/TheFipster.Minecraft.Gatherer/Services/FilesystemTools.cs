using System.IO;

namespace TheFipster.Minecraft.Gatherer.Services
{
    public class FilesystemTools
    {
        public void EnsureFilepathExists(string filepath)
        {
            var info = new FileInfo(filepath);
            
            if (!info.Directory.Exists)
                info.Directory.Create();
        }
    }
}
