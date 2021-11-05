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
using ProjectSayoriRevised;
using ProjectSayoriRevised.Commands.DanbooruApi;


namespace ProjectSayoriRevised.Commands
{
    public class Danbooru : BaseCommandModule
    {

        [Command("danbooru")]
        [Description("Picks a random safe image from danbooru.")]
        public async Task DanbooruImage(CommandContext ctx)
        {

            #region Old Code
            /* var client = new HttpClient();
               var result = await client.GetStringAsync("https://danbooru.donmai.us/posts/random.json?tags=rating%3Asafe");
               JArray arr = JArray.Parse(result);
               JObject post = JObject.Parse(arr.ToString());

               var builder = new DiscordEmbedBuilder
               {
                   Title = "Danbooru Safe Image",
                   Color = DiscordColor.Red,
                   ImageUrl = arr["file_url"].ToString(),
                   Url = "https://danbooru.donmai.us/posts/" + arr["id"].ToString(),
               };
               var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false); */
            #endregion

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