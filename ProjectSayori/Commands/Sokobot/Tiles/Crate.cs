using System.Numerics;
namespace Sokobot
{
    public class Crate
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Tile TileStatus { get; private set; }
        private int origX;
        private int origY;
        private Grid currentGrid;

        public Crate(int x, int y, Grid grid)
        {
            X = x;
            Y = y;
            origX = x;
            origY = y;
            TileStatus = Tile.Crate;
            currentGrid = grid;
        }

        public Vector2 Move(Vector2 direction)
        {
            int x = (int)(X + direction.X);
            int y = (int)(Y + direction.Y);

            if (currentGrid.IsInside(x, y) && !currentGrid.IsCrate(x, y))
            {
                if (currentGrid.IsDestination(x, y))
                    TileStatus = Tile.Done;
                else
                    TileStatus = Tile.Crate;
                
                X = x;
                Y = y;
                return Vector2.One;
            }

            return Vector2.Zero;
        }

        public void Reset()
        {
            X = origX;
            Y = origY;
            TileStatus = Tile.Crate;
        }
    }
}