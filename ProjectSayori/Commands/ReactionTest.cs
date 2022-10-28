using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace ProjectSayori.Commands
{
    public class ReactionTest: BaseCommandModule
    {
        [Command("dab")]
        [Description("Sayori wants to dab")]
        public async Task Dab(CommandContext ctx)
        {
            DiscordEmbedBuilder builder = BuildEmbedMessage("https://cdn.discordapp.com/attachments/870566103561154611/1034125411896016906/sayoridab.jpeg", 
                                                            "She be dabbing!" );
            var msg = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }

        [Command("pat")]
        [Description("Sayori wants a pat")]
        public async Task Pat(CommandContext ctx, [RemainingText] string name)
        {
            DiscordEmbedBuilder builder;
            if (name == null)
            {
                builder = BuildEmbedMessage("https://cdn.discordapp.com/attachments/870566103561154611/1034129828015779950/sayoripat.gif", 
                                            "You gave cinnamon roll a pat!" );      
                var msg = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else
            {
                builder = BuildEmbedMessage("https://cdn.discordapp.com/attachments/870566103561154611/1034129828015779950/sayoripat.gif", 
                                            $"You gave {name} a pat!" );  
                var msg = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            
        }

        [Command("hug")]
        [Description("Sayori wants a hug")]
        public async Task Hug(CommandContext ctx)
        {
            DiscordEmbedBuilder builder = BuildEmbedMessage("https://cdn.discordapp.com/attachments/870566103561154611/1034130518171725886/sayorihug.jpeg", 
                                                            "A warm embrace to Sayori...~" );
            var msg = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
        }
        private DiscordEmbedBuilder BuildEmbedMessage(string url, string title)
        {
            var builder = new DiscordEmbedBuilder
            {
                Description = title,
                Color = DiscordColor.Green,
                ImageUrl = url
            };

            return builder;
        }
    }
    
}