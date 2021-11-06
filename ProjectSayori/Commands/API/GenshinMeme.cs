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
    public class GenshinMeme: BaseCommandModule
    {
        [Command("genshinmeme")]
        [Description("Picks a random meme from r/Genshin_Memepact.")]
        public async Task GenshinMemepact(CommandContext ctx)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://reddit.com/r/Genshin_Memepact/random.json?limit=1");
            JArray arr = JArray.Parse(result);
            JObject post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());

            var builder = new DiscordEmbedBuilder
            {
                Title = post["title"].ToString(),
                Color = DiscordColor.Red,
                ImageUrl = post["url"].ToString(),
                Url = "https://reddit.com" + post["permalink"].ToString()
            };
            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }
    }
}