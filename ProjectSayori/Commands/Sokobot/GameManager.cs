using System;
using System.IO;
using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.Entities;


namespace Sokobot
{
    public class GameManager
    {
        public static Dictionary<ulong, Game> Games { get; } = new Dictionary<ulong, Game>();
        // kindly change this file location to your file location
        // nagloloko kasi pag relative path lang nilalagay ko e
        private static string saveFile = "C:/Users/Angelika Louise/Desktop/C# Projects/ProjectSayori/ProjectSayori/Commands/Sokobot/saves.txt";
        
        public static async void RestoreGameProgress(DiscordClient client)
        {
            var lines = File.ReadAllLines(saveFile);
            foreach (var line in lines)
            {
                var data = line.Split('|');
                var user = await client.GetUserAsync(Convert.ToUInt64(data[0]));
                Games[Convert.ToUInt64(data[0])] = new Game(user, Convert.ToInt32(data[1]));
            }
        }

        public static void SaveGameProgress(DiscordUser user, int level)
        {
            var lines = File.ReadAllLines(saveFile);
            using (StreamWriter writer = new StreamWriter(saveFile, false))
            {
                var hasSave = false;
                foreach (var line in lines)
                {
                    var data = line.Split('|');
                    if (user.Id == Convert.ToUInt64(data[0]))
                    {
                        writer.WriteLine($"{user.Id}|{level}");
                        hasSave = true;
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }

                if (!hasSave)
                    writer.WriteLine($"{user.Id}|{level}");
            }
        }

        public static Game NewGame(DiscordUser user, int level)
        {
            Games[user.Id] = new Game(user, level);
            return Games[user.Id];
        }

        public static Game LoadGame(DiscordUser user)
        {
            return Games[user.Id];
        }
    }
}