using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;

namespace ProjectSayori.Commands
{
    public class Cards52 : BaseCommandModule
    {
        [Command("pull")]
        [Description("Pull numbers of cards")]
        public async Task Pull(CommandContext ctx, [Description("Number of cards")][RemainingText]int x)
        {
            if (x > 52)
            {
                await ctx.RespondAsync("Wait, that's too much!");
            }
            else
            {
                await ctx.RespondAsync(Scout(x));
            }
        }
        static string Scout(int x)
        {
            ArrayList Pull = new ArrayList();
            while (Pull.Count != x)
            {
                string card = ($"{DeckOfCards("")}");
                if (Pull.Contains(card))
                {
                    continue;
                }
                else
                {
                    Pull.Add(card);
                }
            }
            string display = "";

            foreach (object obj in Pull)
            {
                display = display + $"{obj}\n";
            }

            return $"```{display}```";
        }
        static string DeckOfCards(string x)
        {
            var random = new Random();
            int a = random.Next(1, 14);
            int b = random.Next(1, 101);

            string suit = "";

            switch (b)
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
            switch (a)
            {
                case 1:
                    face = "Ace";
                    break;
                case 2:
                    face = "Two";
                    break;
                case 3:
                    face = "Three";
                    break;
                case 4:
                    face = "Four";
                    break;
                case 5:
                    face = "Five";
                    break;
                case 6:
                    face = "Six";
                    break;
                case 7:
                    face = "Seven";
                    break;
                case 8:
                    face = "Eight";
                    break;
                case 9:
                    face = "Nine";
                    break;
                case 10:
                    face = "Ten";
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
            return $"{face} of {suit}";
        }
    }
}