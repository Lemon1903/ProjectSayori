using System.Numerics;


namespace Sokobot
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private int origX;
        private int origY;
        private Grid currentGrid;

        public Player(int x, int y, Grid grid)
        {
            X = x;
            Y = y;
            origX = x;
            origY = y;
            currentGrid = grid;
        }

        public Vector2 Move(Vector2 direction)
        {
            int x = (int)(X + direction.X);
            int y = (int)(Y + direction.Y);

            if (currentGrid.IsInside(x, y))
            {
                if (currentGrid.IsCrate(x, y))
                {
                    var crate = currentGrid.GetCrate(x, y);
                    if (crate.Move(direction) != Vector2.Zero)
                    {
                        X = x;
                        Y = y;
                        return Vector2.One;
                    }
                }
                else
                {
                    X = x;
                    Y = y;
                }
            }

            return Vector2.Zero;
        }

        public void Reset()
        {
            X = origX;
            Y = origY;
        }
    }
}