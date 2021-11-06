using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace ProjectSayori.Commands
{
    public class Greetings : BaseCommandModule
    {
        [Command("greet")]
        [Description("Greets the person mentioned.")]
        public async Task greet(CommandContext ctx, [RemainingText] [Description("The name to mention")]string name)
        {
            if (name == null)
            {
                name = "human";
            }

            var random = new Random();
            var x = random.Next(1, 6);


            switch (x)
            {
                case 1:
                    await ctx.RespondAsync($"Hello, {name}! I am ProjectSayori!");
                    break;
                case 2:
                    await ctx.RespondAsync($"Hey, {name}, you look nice today!");
                    break;
                case 3:
                    await ctx.RespondAsync($"Good day, {name}!");
                    break;
                case 4:
                    await ctx.RespondAsync($"Just to tell that you look nice today, {name}!");
                    break;
                case 5:
                    await ctx.RespondAsync($"Amazing weather, isn't it, {name}?");
                    break;
            }
        }
    }
}