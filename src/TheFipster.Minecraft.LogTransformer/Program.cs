using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace TheFipster.Minecraft.LogTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var readDir = new DirectoryInfo(@"E:\Speedrunning\temp\logs");
            var writeDir = new DirectoryInfo(@"E:\Speedrunning\temp\newlogs");



            var archives = readDir.GetFiles("*.gz");

            var dated = new Dictionary<DateTime, List<FileInfo>>();

            foreach (var archive in archives)
            {
                Console.WriteLine($"Reading {archive.Name}");
                var date = readDate(archive);

                if (dated.ContainsKey(date))
                    dated[date].Add(archive);
                else
                    dated[date] = new List<FileInfo>() { archive };
            }

            foreach (var dateBundle in dated)
            {
                var count = dateBundle.Value.Count();
                var start = new DateTimeOffset(dateBundle.Key).ToUnixTimeSeconds();
                var end = new DateTimeOffset(dateBundle.Key + new TimeSpan(23, 59, 59)).ToUnixTimeSeconds();
                var step = (end - start) / (count * 2);
                var time = start + step;

                foreach (var archive in dateBundle.Value)
                {

                    var filename = $"{time}.log";
                    var path = Path.Combine(writeDir.FullName, filename);
                    using (var archiveStream = archive.OpenRead())
                    using (var memoryStream = new MemoryStream())
                    using (var decompressionStream = new GZipStream(archiveStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(memoryStream);
                        var buffer = new byte[memoryStream.Length];
                        memoryStream.Position = 0;
                        memoryStream.Read(buffer, 0, buffer.Length);
                        File.WriteAllBytes(path, buffer);
                    }

                    time += step;
                }
            }
        }

        private static DateTime readDate(FileInfo archive)
        {
            var filename = archive.Name;

            var year = int.Parse(filename.Substring(0, 4));
            var month = int.Parse(filename.Substring(5, 2));
            var day = int.Parse(filename.Substring(8, 2));

            return new DateTime(year, month, day);
        }
    }
}
