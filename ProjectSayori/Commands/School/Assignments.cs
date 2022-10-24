using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json.Linq;
using DSharpPlus.Entities;

namespace ProjectSayori.Commands
{
    public class Assignments : BaseCommandModule
    {

        private static readonly string[] subjects = new string[]
        {
            /*0*/ "Discrete Structures",
            /*1*/ "World Literature",
            /*2*/ "Data Structures and Algorithms",
            /*3*/ "Object Oriented Programming",
            /*4*/ "Ethics",
            /*5*/ "Individual/Dual/Combative Sports",
            /*6*/ "Modeling and Simulation",
            /*7*/ "Logic Design and Digital Computer Circuits",
            /*8*/ "CS Free Elective"
        };
        
        [Command("homework")]
        [Description("Displays the upcoming assignments, as well as its deadlines.")]
        public async Task Ass(CommandContext ctx)
        {
            string[] homeworks = File.ReadAllLines(@"D:\homework.txt");
            DateTime now = DateTime.Now;
            var builder = new DiscordEmbedBuilder
            {
                Title = "Assignments",
                Color = DiscordColor.Red,
                Description = $"Here are the homeworks needed to be passed."
            };

            for (int i = 0; i < homeworks.Length; i += 3)
            {
                builder.AddField(subjects[Int32.Parse(homeworks[i])], $"{homeworks[i+1]}\n*{homeworks[i+2]}*");
            }
            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }

        [Command("homework")]
        [Description("Displays the upcoming assignments, as well as its deadlines.")]
        public async Task Ass(CommandContext ctx, string input)
        {
            if (input.Equals("add", StringComparison.OrdinalIgnoreCase))
            {
                var interactivity = ctx.Client.GetInteractivity();

                // Displays subjects
                DiscordEmbedBuilder builder = BuildEmbedMessage(DiscordColor.Blue);
                var message = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);

                // Waits for the subject input.
                await ctx.RespondAsync($"What subject?");
                var subjects = BuildEmbedMessage(DiscordColor.Red);
                var msg = await interactivity.WaitForMessageAsync(x => true);
                var subjectContent = msg.Result.Content;

                // Waits for the description input.
                await ctx.RespondAsync($"What to pass?");
                msg = await interactivity.WaitForMessageAsync(x => true);
                var descriptionContent = msg.Result.Content;

                // Wait for the deadline input.
                await ctx.RespondAsync($"When's the deadline?");
                msg = await interactivity.WaitForMessageAsync(x => true);
                var deadlineContent = msg.Result.Content;
                
                // Displays confirmation
                try
                {
                    DiscordEmbedBuilder confirmMsg = BuildEmbedMessage(Convert.ToInt32(subjectContent), descriptionContent, deadlineContent);
                    message = await ctx.Channel.SendMessageAsync(embed: confirmMsg).ConfigureAwait(false);
                }
                catch
                {
                    await ctx.RespondAsync($"An error has occured. Please make sure you input the correct subject number.");
                    return;
                }
                
                // Add emojis to the confirmation embed
                var yes = DiscordEmoji.FromName(ctx.Client, ":green_circle:");
                await message.CreateReactionAsync(yes).ConfigureAwait(false);
                var no = DiscordEmoji.FromName(ctx.Client, ":red_circle:");
                await message.CreateReactionAsync(no).ConfigureAwait(false);
                await Task.Delay(300);

                // Waits for the emoji interaction.
                var confirm = await interactivity.WaitForReactionAsync(x => true).ConfigureAwait(false);

                if (confirm.Result.Emoji == no)
                {
                    await ctx.RespondAsync($"Action cancelled.");
                    return;
                }

                // Add homework to the file.
                await ctx.RespondAsync($"Homework has been added to the database.");
                WriteFile(Convert.ToInt32(subjectContent), descriptionContent, deadlineContent);

            }
        }
        
        // Subjects Reveal
        private DiscordEmbedBuilder BuildEmbedMessage(DiscordColor color)
        {
            var builder = new DiscordEmbedBuilder
            {
                Title = "Subjects",
                Color = color,
                Description = $"Input the number."
            };

            for (int i = 0; i < subjects.Length; i++)
            {
                builder.AddField($"{i}:", $"{subjects[i]}");
            }
            return builder;
        }

        private DiscordEmbedBuilder BuildEmbedMessage(int sub, string description, string deadline)
        {
            var builder = new DiscordEmbedBuilder
            {
                Title = "Homework to be added",
                Color = DiscordColor.Green,
                Description = "React to the emojis below to confirm."
            };

            builder.AddField($"{subjects[sub]}", $"{description}\n*{deadline}*");

            return builder;
        }

        private void WriteFile(int sub, string description, string deadline)
        {
            string filepath = @"D:\homework.txt";
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine(sub);
                sw.WriteLine(description);
                sw.WriteLine(deadline);
                sw.Close();
            }
        }
    }
}
