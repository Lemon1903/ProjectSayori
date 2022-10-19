using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
            string[] homeworks = System.IO.File.ReadAllLines(@"D:\win10\Documents\Discord Bot\ProjectSayori\ProjectSayori\Commands\School\Homework.txt");
            DateTime now = DateTime.Now;
            var builder = new DiscordEmbedBuilder
            {
                Title = "Assignments",
                Color = DiscordColor.Red,
                Description = $"Last Updated: 02/03/2022"
            };

            for (int i = 0; i < homeworks.Length; i += 4)
            {
                builder.AddField(subjects[Int32.Parse(homeworks[i])], $"{homeworks[i+1]}\n{homeworks[i+2]}\n{homeworks[i+3]}");
            }
            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }
    }
}
