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
    public class Exams : BaseCommandModule
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

        [Command("exam")]
        [Description("Exams? Lol, are you reviewing?")]
        public async Task Exam(CommandContext ctx)
        {
            DateTime now = DateTime.Now;
            var builder = new DiscordEmbedBuilder
            {
                Title = "Exams",
                Color = DiscordColor.Red,
                Description = $"Here is the schedule of the exams as of 12/14."
            };

            builder.AddField(subjects[0], "December 16\nCoverage: From start to Array");
            builder.AddField(subjects[1], "The Group Activity");
            builder.AddField(subjects[3], "The Reporting");
            builder.AddField(subjects[4], "December 17 (Whole Day/Take Home)");
            builder.AddField(subjects[5], "December 14\nCoverage: From start to Number Systems");
            builder.AddField(subjects[6], "December 17");

            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }
    }
}
