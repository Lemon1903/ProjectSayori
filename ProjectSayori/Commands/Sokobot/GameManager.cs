using System.Collections.Generic;
using DSharpPlus.Entities;
using Sokobot;


namespace Sokobot
{
    public class GameManager
    {
        public static Dictionary<DiscordUser, Game> Games { get; private set; } = new Dictionary<DiscordUser, Game>();
        
        public static Game NewGame(DiscordUser user, int level)
        {
            Games[user] = new Game(user, level);
            return Games[user];
        }

        public static Game LoadGame(DiscordUser user)
        {
            return Games[user];
        }
    }
}