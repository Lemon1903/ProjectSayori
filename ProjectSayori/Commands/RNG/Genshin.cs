using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace ProjectSayori.Commands
{

    public class Genshin : BaseCommandModule
    {
        [Command("wish")]
        [Description("A Wish System from Genshin Impact.")]
        public async Task Pull(CommandContext ctx, [Description("<standard | character | weapon>")]string banner,
                                                   [Description("<1 | 10>")]int wishes)
        {
            restart:
            if ((banner != "standard" & banner != "character" & banner != "weapon") | banner == null)
            {
                await ctx.RespondAsync("```?wish <standard | character | weapon> <1 | 10>```");
            }
            else if (wishes != 1 & wishes != 10)
            {
                await ctx.RespondAsync("Wish 1 or Wish 10 only.");
            }
            else
            {
                await ctx.RespondAsync("```5* Pity is still in development.```");
                await ctx.RespondAsync($"{Scout(wishes, banner)}");
                await ctx.RespondAsync($"Enter 'wish' to continue wishing {wishes}.");

                var results = await ctx.Message.GetNextMessageAsync(m =>
                {
                    return m.Content.ToLower() == "wish";
                });

                if (!results.TimedOut) goto restart;
            }
        }

        int fourStarPity = 0;
        int fiveStarPity = 0;

        public string Scout(int x, string y)
        {
            ArrayList Pull = new ArrayList();
            string card = "";
            switch (y)
            {
                case "standard":
                    
                    while (Pull.Count != x)
                    {
                        fourStarPity++;
                        fiveStarPity++;

                        if (fiveStarPity == 90)
                        {
                            card = ($"{fiveStarExceed()}");
                        }
                        else if (fourStarPity >= 10)
                        {
                            card = ($"{fourStarExceed()}");
                        }
                        else
                        {
                            card = ($"{Standard()}");
                        }

                        if (card.Contains("4★"))
                        {
                            fourStarPity = 0;
                        }
                        if (card.Contains("5★"))
                        {
                            fiveStarPity = 0;
                        }
                        Pull.Add($"{card}");
                    }
                    break;
            }

            string display = "";

            foreach (object obj in Pull)
            {
                display = display + $"{obj}\n";
            }

            return $"```{display}```";
        }
        

        
        // Item Listings

        List<string> fiveStars = new List<string>()
            {
                "Jean",
                "Diluc",
                "Keqing",
                "Mona",
                "Qiqi",
                "Amos' Bow",
                "Aquila Favonia",
                "Skyward Harp",
                "Skyward Spine",
                "Skyward Atlas",
                "Skyward Pride",
                "Skyward Blade",
                "Wolf's Gravestone",
                "Primordial Jade Winged-Spear",
                "Lost Prayers to the Sacred Wings"
            };
        List<string> fourStars = new List<string>()
            {
                "Sayu",
                "Yanfei",
                "Rosaria",
                "Xinyan",
                "Sucrose",
                "Diona",
                "Chongyun",
                "Noelle",
                "Bennett",
                "Fischl",
                "Ningguang",
                "Xingqui",
                "Beidou",
                "Xiangling",
                "Amber",
                "Razor",
                "Kaeya",
                "Barbara",
                "Lisa",
                "Sacrificial Bow",
                "The Stringless",
                "Favonius Warbow",
                "Eye of Perception",
                "Sacrificial Fragments",
                "The Widsith",
                "Favonius Codex",
                "Favonius Lance",
                "Dragon's Bane",
                "Rainslasher",
                "Sacrificial Greatsword",
                "The Bell",
                "Favonius Greatsword",
                "Lion's Roar",
                "Sacrificial Sword",
                "The Flute",
                "Favonius Sword"
            };
        List<string> threeStars = new List<string>()
            {
                "Slingshot",
                "Sharpshooter's Oath",
                "Raven Bow",
                "Emerald Orb",
                "Thrilling Tales of Dragon Slayers",
                "Magic Guide",
                "Black Tassel",
                "Debate Club",
                "Bloodtainted Greatsword",
                "Ferrous Shadow",
                "Skyrider Sword",
                "Harbinger of Dawn",
                "Cool Steel"
            };


        // RNG Logic

        public string Standard()
        {
            var random = new Random();
            int a = random.Next(1, 1001);

            string name = "";
            int star = 0;

            if (a <= 6)
            {
                star = 5;
            }
            else if (a > 6 & a <= 57)
            {
                star = 4;
            }
            else
            {
                star = 3;
            }

            int randomItem = 0;
            switch (star)
            {
                case 5:
                    randomItem = random.Next(0, fiveStars.Count);
                    name = fiveStars[randomItem];
                    break;
                case 4:
                    randomItem = random.Next(0, fourStars.Count);
                    name = fourStars[randomItem];
                    break;
                case 3:
                    randomItem = random.Next(0, threeStars.Count);
                    name = threeStars[randomItem];
                    break;
            }
            return $"[{star}★] {name}";
        }
        
        // Pities
        public string fourStarExceed()
        {
            string name = "";

            var random = new Random();
            int randomItem = 0;
            randomItem = random.Next(0, fourStars.Count);
            name = fourStars[randomItem];

            return $"[4★] {name} (pity)";
        }
        public string fiveStarExceed()
        {
            string name = "";

            var random = new Random();
            int randomItem = 0;
            randomItem = random.Next(0, fiveStars.Count);
            name = fourStars[randomItem];

            return $"[5★] {name} (pity)";
        }
    }
}