using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Net;
using ProjectSayori.Commands;
using ProjectSayori.Commands.QuoteGenerator;
using Sokobot;


namespace ProjectSayori
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
            });

            /* ============== Interactivity ============== */
            discord.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(10)
            });

            /* ================= Commands ================= */
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" }
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

            // Sokobot (Game)
            commands.RegisterCommands<SokobotCommand>();

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

            /* ================== Events ================== */
            discord.ComponentInteractionCreated += ButtonEvent.ButtonPressed;

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}