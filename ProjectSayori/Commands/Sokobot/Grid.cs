using System;
using System.Numerics;


namespace Sokobot
{
    public class Grid
    {
        public int CratesLeft { get; private set; }
        public Player CurrentPlayer { get; }
        public Crate[] Crates { get; }
        public Destination[] Destinations { get; }
        public Tile[,] grid { get; }

        private int maxTarget;
        private readonly Random random = new Random();
        private const int ROWS = 8;
        private const int COLUMNS = 13;

        // constructor
        public Grid()
        {
            maxTarget = random.Next(1, 4);
            grid = new Tile[ROWS, COLUMNS];
            CratesLeft = maxTarget;
            Crates = new Crate[maxTarget];
            Destinations = new Destination[maxTarget];

            // place walls
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    if (i == 0 || i == ROWS - 1 || j == 0 || j == COLUMNS - 1)
                        grid[i, j] = Tile.Walls;
                }
            }

            // place the boxes
            int cratesCount = 0;
            while (cratesCount < maxTarget)
            {
                var boxPos = new Vector2(random.Next(2, ROWS - 2), random.Next(2, COLUMNS - 2));
                if (grid[(int)boxPos.X, (int)boxPos.Y] == Tile.Ground)
                {
                    Crates[cratesCount] = new Crate((int)boxPos.X, (int)boxPos.Y, this);
                    grid[(int)boxPos.X, (int)boxPos.Y] = Tile.Crate;
                    cratesCount++;
                }
            }

            // place the target tiles
            int targetCount = 0;
            while (targetCount < maxTarget)
            {
                var targetPos = new Vector2(random.Next(1, ROWS - 1), random.Next(1, COLUMNS - 1));
                if (grid[(int)targetPos.X, (int)targetPos.Y] == Tile.Ground)
                {
                    Destinations[targetCount] = new Destination((int)targetPos.X, (int)targetPos.Y);
                    grid[(int)targetPos.X, (int)targetPos.Y] = Tile.Target;
                    targetCount++;
                }
            }

            // place the player
            while (CurrentPlayer == null)
            {
                var playerPos = new Vector2(random.Next(1, ROWS - 1), random.Next(1, COLUMNS - 1));
                if (grid[(int)playerPos.X, (int)playerPos.Y] == Tile.Ground)
                {
                    CurrentPlayer = new Player((int)playerPos.X, (int)playerPos.Y, this);
                    grid[(int)playerPos.X, (int)playerPos.Y] = Tile.Player;
                }
            }
        }

        public void UpdateGrid()
        {
            // make all tiles ground first
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLUMNS; j++)
                {
                    if (i == 0 || i == ROWS - 1 || j == 0 || j == COLUMNS - 1)
                        grid[i, j] = Tile.Walls;
                    else
                        grid[i, j] = Tile.Ground;
                }
            }

            // make the destination tile target tile
            for (int i = 0; i < maxTarget; i++)
                grid[Destinations[i].X, Destinations[i].Y] = Tile.Target;

            // same goes with the box
            CratesLeft = 0;
            for (int i = 0; i < maxTarget; i++)
            {
                if (Crates[i].TileStatus == Tile.Crate)
                    CratesLeft++;

                grid[Crates[i].X, Crates[i].Y] = Crates[i].TileStatus;
            }
            
            // and lastly the player
            grid[CurrentPlayer.X, CurrentPlayer.Y] = Tile.Player;
        }

        public string ConvertToString()
        {
            string[] gridString = new string[ROWS];
            
            for (int i = 0; i < ROWS; i++)
            {
                string temp = String.Empty;
                for (int j = 0; j < COLUMNS; j++)
                    temp += TilesEmoji.Emojis[((int)grid[i, j])];
                
                gridString[i] = temp;
            }

            return string.Join('\n', gridString);
        }

        public Crate GetCrate(int x, int y)
        {
            foreach (var crate in Crates)
            {
                if (x == crate.X && y == crate.Y)
                    return crate;
            }

            return null;
        }

        public bool IsInside(int x, int y)
        {
            return grid[x, y] != Tile.Walls;
        }

        public bool IsCrate(int x, int y)
        {
            return grid[x, y] == Tile.Crate || grid[x, y] == Tile.Done;
        }

        public bool IsDestination(int x, int y)
        {
            return grid[x, y] == Tile.Target;
        }

        public void Reset()
        {
            CratesLeft = maxTarget;
            CurrentPlayer.Reset();
            foreach (var crate in Crates)
                crate.Reset();
        }
    }
}