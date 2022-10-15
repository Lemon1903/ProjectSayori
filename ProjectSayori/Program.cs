using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Net;
using ProjectSayori.Commands;
using ProjectSayori.Commands.QuoteGenerator;


namespace ProjectSayori
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
            // Test();
        }


        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "MTAyOTc1Njg4MzgzMTA5OTU2Mg.GSwONI.EcZlqwTqH_PipGwB5FrjTCjjlz2WMyANLqvOB8",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            });

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
            });

            discord.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(10)
            });

            // RNG
            commands.RegisterCommands<Greetings>();
            commands.RegisterCommands<Dice>();
            commands.RegisterCommands<Cards>();
            commands.RegisterCommands<Cards52>();
            commands.RegisterCommands<Genshin>();

            // API
            commands.RegisterCommands<Danbooru>();
            commands.RegisterCommands<GenshinHentai>();
            commands.RegisterCommands<GenshinMeme>();
            commands.RegisterCommands<GenshinImage>();
            commands.RegisterCommands<Memes>();

            // Quote Generator
            commands.RegisterCommands<RandomQuoteCommand>();
            commands.RegisterCommands<SearchQuoteCommand>();

            // Math Related
            commands.RegisterCommands<Fibonacci>();
            commands.RegisterCommands<EuclideanAlgorithm>();
            commands.RegisterCommands<NumberSystem>();

            // Games
            commands.RegisterCommands<Touhou>();

            // For my class
            commands.RegisterCommands<Schedule>();
            commands.RegisterCommands<ScheduleCopy>();
            commands.RegisterCommands<Assignments>();
            commands.RegisterCommands<Exams>();

            // Others
            commands.RegisterCommands<InputTest>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        #region Testing
        // private static void Test()
        // {
        //     Dictionary<string, string[,]> dict = new()
        //     {
        //         { "hi", new string[,] 
        //             { 
        //                 {"sdfsd", "sdfsdf", "sdfsd"}, 
        //                 {"asdasd", "adsasd", "sasd"} 
        //             } 
        //         },

        //         { "hello", new string[,] 
        //             { 
        //                 {"sdfsd", "sdfsdf", "sdfsd"}, 
        //                 {"asdasd", "adsasd", "sasd"} 
        //             } 
        //         },
        //     };

        //     for (int i = 0; i < dict["hi"].GetLength(0); i++)
        //     {
        //         Console.WriteLine(dict["hi"][i, 0]);
        //     }
        //     Console.ReadLine();
        // }
        #endregion
    }
}