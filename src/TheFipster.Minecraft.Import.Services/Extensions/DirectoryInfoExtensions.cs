using System.IO;
using System.Linq;

namespace TheFipster.Minecraft.Import.Services.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static long GetSize(this DirectoryInfo dir) => dir
            .EnumerateFiles("*", SearchOption.AllDirectories)
            .Sum(file => file.Length);
    }
}
