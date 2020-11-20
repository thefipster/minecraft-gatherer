using Cyotek.Data.Nbt;
using System;

namespace TheFipster.Minecraft.Gatherer.NbtConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Failed with invalid tag id exception
            var doc = NbtDocument.LoadDocument(@"E:\Speedrunning\server\world-1605387558\playerdata\b86cee08-ad4c-4452-995f-3c3661a504b5.dat");
        }
    }
}
