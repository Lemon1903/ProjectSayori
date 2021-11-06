using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json.Linq;
using DSharpPlus.Entities;
using ProjectSayori;
using ProjectSayori.Commands.DanbooruApi;


namespace ProjectSayori.Commands
{
    public class Danbooru : BaseCommandModule
    {

        [Command("danbooru")]
        [Description("Picks a random safe image from danbooru.")]
        public async Task DanbooruImage(CommandContext ctx)
        {

            ApiHelper.InitializeClient();

            var image = await ImageProcessor.LoadImage();
            var uriSource = new Uri(image.File_Url, UriKind.Absolute);
            var idSource = image.Id;
            var copyrightSource = image.Tag_String_Copyright;
            var artist = image.Tag_String_Artist;

            var builder = new DiscordEmbedBuilder
            {
                Title = "Here's a Random Danbooru Safe Image!",
                Color = DiscordColor.Green,
                ImageUrl = uriSource.ToString(),
                Url = "https://danbooru.donmai.us/posts/" + idSource.ToString(),
            };
            builder.AddField("Sauce", copyrightSource.ToString());
            builder.AddField("Artist", artist.ToString());
            var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);

        }
    }
}