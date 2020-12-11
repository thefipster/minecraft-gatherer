namespace TheFipster.Minecraft.Core.Domain
{
    public class Coordinate
    {
        public Coordinate() { }

        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
