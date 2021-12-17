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
    public class GenshinImage : BaseCommandModule
    {
        [Command("genshinimage")]
        [Description("Picks a random Genshin image from Danbooru.")]
        public async Task Genshin(CommandContext ctx, [RemainingText]string x)
        {

            ApiHelper.InitializeClient();
            try
            {
                var image = await ImageProcessor.LoadImageGenshinImage(x);
                var uriSource = new Uri(image.File_Url, UriKind.Absolute);
                var idSource = image.Id;
                var copyrightSource = image.Tag_String_Copyright;
                var artist = image.Tag_String_Artist;

                string Title;
                if (x == null)
                {
                    Title = "Here's a Random Safe Genshin Image from Danbooru.";
                }
                else
                {
                    Title = $"Here's a Random Safe {x.ToUpper()} Image from Danbooru.";
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
            catch
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
            
        }
    }
}