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

        string[] subjects =
        {
            /*0*/ "Computer Programming 1",
            /*1*/ "Physical Fitness and Self-Testing Activities",
            /*2*/ "Filipinolohiya at Pambansang Kaunlaran",
            /*3*/ "Purposive Communication",
            /*4*/ "Politics, Governance and Citizenship",
            /*5*/ "Introduction to Computing",
            /*6*/ "Mathematics in the Modern World",
            /*7*/ "Civic Welfare Training Service 1"
        };
        

        [Command("pwet")]
        [Description("Displays the upcoming assignments, as well as its deadlines.")]
        public async Task Ass(CommandContext ctx)
        {
            string[] homeworks = System.IO.File.ReadAllLines(@"Commands\School\Homework.txt");
            DateTime now = DateTime.Now;
            var builder = new DiscordEmbedBuilder
            {
                Title = "Assignments",
                Color = DiscordColor.Red,
                Description = $"Last Updated: 12/06/2021"
            };

            for (int i = 0; i < homeworks.Length; i += 4)
            {
                builder.AddField(subjects[Int32.Parse(homeworks[i])], $"{homeworks[i+1]}\n{homeworks[i+2]}\n{homeworks[i+3]}");
            }
            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            
        }
    }
}
