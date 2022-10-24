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
            string[] hw = File.ReadAllLines(".\\homework.txt");

            var homeworks = new Dictionary<int, string[]>();
            for (int i = 0; i < hw.Length; i += 3)
            {
                int id = i/3;
                homeworks.Add(id, new string[] 
                {
                    hw[i],
                    hw[i+1], 
                    hw[i+2]
                });
            }
            
            DiscordEmbedBuilder builder = BuildEmbedMessage(homeworks, "Here are the list of homeworks to be passed");
            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }

        [Command("homework")]
        [Description("Displays the upcoming assignments, as well as its deadlines.")]
        public async Task Ass(CommandContext ctx, string input)
        {
            var interactivity = ctx.Client.GetInteractivity();

            if (input.Equals("add", StringComparison.OrdinalIgnoreCase))
            {

                // Displays subjects
                DiscordEmbedBuilder builder = BuildEmbedMessage(DiscordColor.Blue);
                var message = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);

                // Waits for the subject input.
                await ctx.RespondAsync($"What subject?");
                var subjects = BuildEmbedMessage(DiscordColor.Red);
                var msg = await ctx.Message.GetNextMessageAsync(x => true);
                var subjectContent = msg.Result.Content;

                // Waits for the description input.
                await ctx.RespondAsync($"What to pass?");
                msg = await ctx.Message.GetNextMessageAsync(x => true);
                var descriptionContent = msg.Result.Content;

                // Wait for the deadline input.
                await ctx.RespondAsync($"When's the deadline?");
                msg = await ctx.Message.GetNextMessageAsync(x => true);
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
                WriteFile(Convert.ToInt32(subjectContent), descriptionContent, deadlineContent);
                await ctx.RespondAsync($"Homework has been added to the database.");

            }
            if (input.Equals("delete", StringComparison.OrdinalIgnoreCase))
            {
                string[] hw  = File.ReadAllLines(".\\homework.txt");

                var homeworks = new Dictionary<int, string[]>();
                for (int i = 0; i < hw.Length; i += 3)
                {
                    int id = i/3;
                    homeworks.Add(id, new string[] 
                    {
                        hw[i],
                        hw[i+1], 
                        hw[i+2]
                    });
                }

                DiscordEmbedBuilder builder = BuildEmbedMessage(homeworks, "Input the ID of the homework to be deleted.");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);

                // ID
                await ctx.RespondAsync($"Input the ID of the homework to be deleted.");
                var msg = await ctx.Message.GetNextMessageAsync(x => true);
                var IDm = msg.Result.Content;
                int ID = Int32.Parse(IDm);

                if (!homeworks.ContainsKey(ID))
                {
                    await ctx.RespondAsync($"Cannot find ID.");
                    return;
                }

                DiscordEmbedBuilder confirmMsg = BuildEmbedMessage(Convert.ToInt32(homeworks[ID][0]), homeworks[ID][1], homeworks[ID][2]);

                var message = await ctx.Channel.SendMessageAsync(embed: confirmMsg).ConfigureAwait(false);

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
                RemoveHomework(homeworks, ID);
                await ctx.RespondAsync($"Homework #{ID} has been deleted.");
            }
        }
            
        // List of Homeworks Embed
        private DiscordEmbedBuilder BuildEmbedMessage(Dictionary<int, string[]> homeworks, string description)
        {
            var builder = new DiscordEmbedBuilder
            {
                Title = "Homeworks",
                Color = DiscordColor.Red,
                Description = description
            };

            for (int i = 0; i < homeworks.Count; i++)
            {
                builder.AddField(subjects[Int32.Parse(homeworks[i][0])], $"ID#{homeworks.ElementAt(i).Key}: {homeworks[i][1]}\n*{homeworks[i][2]}*");
            }

            return builder;
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

        // Confirmation Embed
        private DiscordEmbedBuilder BuildEmbedMessage(int sub, string description, string deadline)
        {
            var builder = new DiscordEmbedBuilder
            {
                Title = "Homework",
                Color = DiscordColor.Green,
                Description = "React to the emojis below to confirm."
            };

            builder.AddField($"{subjects[sub]}", $"{description}\n*{deadline}*");

            return builder;
        }

        private void WriteFile(int sub, string description, string deadline)
        {
            string filepath = ".\\homework.txt";
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine(sub);
                sw.WriteLine(description);
                sw.WriteLine(deadline);
                sw.Close();
            }
        }
        private void RemoveHomework(Dictionary<int, string[]> homeworks, int ID)
        {
            string filepath = ".\\homework.txt";
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                for (int i = 0; i < homeworks.Count; i++)
                {
                    if (homeworks.ElementAt(i).Key == ID) continue;
                    sw.WriteLine($"{homeworks[i][0]}");
                    sw.WriteLine($"{homeworks[i][1]}");
                    sw.WriteLine($"{homeworks[i][2]}");
                }
            }
        }
    
    }
}
