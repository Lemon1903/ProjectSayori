using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace ProjectSayori.Commands
{
    public class Dice : BaseCommandModule
    {
        [Command("roll")]
        [Description("Rolls two dices.")]
        public async Task roll(CommandContext ctx)
        {
            var random = new Random();
            var x = random.Next(1, 7);
            var y = random.Next(1, 7);


            await ctx.RespondAsync($"You rolled {x + y}. (Dices: {x} {y})");

        }
    }
}