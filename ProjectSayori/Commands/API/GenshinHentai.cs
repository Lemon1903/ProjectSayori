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
using ProjectSayori.Commands.DanbooruApi;

namespace ProjectSayori.Commands
{
    public class GenshinHentai : BaseCommandModule
    {
        [Command("genshinhentai")]
        [Description("Picks a random hentai from Danbooru.")]
        [RequireNsfw]
        public async Task GenshinNSFW(CommandContext ctx, [RemainingText]string x)
        {

            ApiHelper.InitializeClient();

            var image = await ImageProcessor.LoadImageGenshinNSFW(x);
            if (image == null)
            {
                var error = new DiscordEmbedBuilder
                {
                    Title = "An error has occured",
                    Color = DiscordColor.Red,
                    Description = "Are you sure you typed the correct character?\n" +
                    "If this character contains a space, replace it with an underscore `e.g.: hu_tao`"
                };
                var errorEmbed = await ctx.Channel.SendMessageAsync(embed: error).ConfigureAwait(false);
            }
            else
            {
                var uriSource = new Uri(image.File_Url, UriKind.Absolute);
                var idSource = image.Id;
                var copyrightSource = image.Tag_String_Copyright;
                var artist = image.Tag_String_Artist;

                string Title;
                if (x == null)
                {
                    Title = "Here's a Random Genshin Hentai from Danbooru.";
                }
                else
                {
                    Title = $"Here's a Random {x.ToUpper()} Hentai from Danbooru.";
                }

                var builder = new DiscordEmbedBuilder
                {
                    Title = Title,
                    Color = DiscordColor.Red,
                    ImageUrl = uriSource.ToString(),
                    Url = "https://danbooru.donmai.us/posts/" + idSource.ToString(),
                };
                builder.AddField("Sauce", copyrightSource.ToString());
                builder.AddField("Artist", artist.ToString());

                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
        }
    }
}