using System;
using System.IO;

namespace TheFipster.Minecraft.Core.Abstractions
{
    public interface IConfigService
    {
        DirectoryInfo ServerLocation { get; }
        DirectoryInfo LogLocation { get; }
        DirectoryInfo TempLocation { get; }
        DirectoryInfo DataLocation { get; }
        FileInfo PythonLocation { get; }
        FileInfo NbtConverterLocation { get; }
        FileInfo OverviewerLocation { get; }
        Uri OverviewerUrl { get; }
        int InitialRunIndex { get; }
    }
}
