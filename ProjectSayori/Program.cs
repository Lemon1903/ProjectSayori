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
                Token = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
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
            commands.RegisterCommands<Assignments>();
            commands.RegisterCommands<Exams>();

            // Others
            commands.RegisterCommands<InputTest>();
            commands.RegisterCommands<ReactionTest>();


            /* ================== Events ================== */
            discord.ComponentInteractionCreated += ButtonEvent.ButtonPressed;

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

        #region Testing
        private static void Test()
        {
            string[,] sched = new string[,] 
            {
                { "subject 4", "09:00 - 10:30" },
                { "subject 5", "10:30 - 13:30" },
                { "(No Subject)", "13:30 - 15:00" },
                { "subject 6", "15:00 - 16:30" },
            };
            string[] links = new string[]
            {
                "https://meet.google.com/twn-czpk-ezw",
                "No link",
                "https://us02web.zoom.us/j/81384059714?pwd=WEt1NmZTcUIzWUkrajZvQ1N6OHRHZz09",
            };

            // get all the times with subject first
            List<TimeSpan[]> subjectTimes = new();
            for (int i = 0; i < sched.GetLength(0); i++)
            {
                if (sched[i, 0] != "(No Subject)")
                {
                    var timeArr = from string subjectTime in sched[i, 1].Split("-") select TimeSpan.Parse(subjectTime);
                    subjectTimes.Add(timeArr.ToArray());
                }
            }

            // DateTime now = DateTime.Now;
            TimeSpan t = new(16, 20, 0);
            if (t < subjectTimes[0][0])
                Console.WriteLine("too early for classes");

            for (int i = 0; i < subjectTimes.Count; i++)
            {
                if (t >= subjectTimes[i][0] && t <= subjectTimes[i][1])
                    Console.WriteLine(links[i]);
            }

            Console.WriteLine("No subject");
            Console.ReadLine();
        }

        private static void Test1()
        {
            string s = "Computer Programming";
            Console.WriteLine(s.IndexOf("computer", StringComparison.OrdinalIgnoreCase) >= 0);
            Console.ReadLine();
        }
        #endregion
    }
}