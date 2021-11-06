using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace ProjectSayori.Commands
{
    public class Cards : BaseCommandModule
    {
        [Command("pick")]
        [Description("Picks a single card from the deck.")]
        public async Task pick(CommandContext ctx)
        {
            var random = new Random();
            int x = random.Next(1, 14);
            int y = random.Next(1, 101);

            string suit = "";

            switch (y)
            {
                case <= 25:
                    suit = "Hearts";
                    break;
                case <= 50:
                    suit = "Diamonds";
                    break;
                case <= 75:
                    suit = "Spades";
                    break;
                case <= 100:
                    suit = "Clubs";
                    break;
            }

            string face = "";
            switch (x)
            {
                case 1:
                    face = "Ace";
                    break;
                case <= 10:
                    face = $"{x}";
                    break;
                case 11:
                    face = "Jack";
                    break;
                case 12:
                    face = "Queen";
                    break;
                case 13:
                    face = "King";
                    break;
            }

            await ctx.RespondAsync($"{face} of {suit}");
        }
    }
}