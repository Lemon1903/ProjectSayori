using System.Collections;
using System.Collections.Generic;

namespace Sokobot
{
    public enum Tile
    {
        Ground, 
        Walls,
        Player, 
        Crate, 
        Target, 
        Done,
    }
    
    public struct TilesEmoji
    {
        public static List<string> Emojis { get; } = new List<string>(){
            ":black_large_square:",
            ":blue_square:",
            ":grinning:",
            ":brown_square:",
            ":red_square:",
            ":green_square:",
        };
    }
}