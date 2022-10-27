namespace Sokobot
{
    public class Destination
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Destination(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}