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
            DateTime now = DateTime.Now;
            var builder = new DiscordEmbedBuilder
            {
                Title = "Assignments",
                Color = DiscordColor.Red,
                Description = $"Last Updated: 12/06/2021"
            };

            builder.AddField(subjects[2], "> Basahin ang modyul at pumili ng dalawang gawain na nais mong sagutan\n> mula sa Gawan 2, 3, at 6 \n" +
                "> Ang Modyul: https://drive.google.com/file/d/1dtm0zohDOGAKttr_6aDbXDAonkF29fst/view?usp=sharing" +
                "\n*01/??/2022*");
            builder.AddField(subjects[7], "> Answer Module 1 Assessment in Google Classroom" +
                "\n*11/??/21*");
            builder.AddField(subjects[6], "> Quiz! From Patterns to Propositions" +
                "\n> Make your own truth table example." +
                "\n*1?/??/2021 (Before Vacation)*");
            builder.AddField(subjects[1], "> Student's Fitness Journal (FINAL EXAM)" +
                "\n*02/14/2022*");

            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }
    }
}
