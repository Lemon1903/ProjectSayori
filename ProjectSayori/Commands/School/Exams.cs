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
        [Command("exam")]
        [Description("Exams? Lol, are you reviewing?")]
        public async Task Exam(CommandContext ctx)
        {
            DateTime now = DateTime.Now;
            var builder = new DiscordEmbedBuilder
            {
                Title = "Exams",
                Color = DiscordColor.Red,
                Description = $"You're too excited..."
            };

            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }
    }
}
