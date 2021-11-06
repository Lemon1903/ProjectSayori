using System;
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
    public class InputTest : BaseCommandModule
    {
        [Command("input")]
        [Description("Copies the next message sent by anyone.")]
        public async Task ActionCommand(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = await interactivity.WaitForMessageAsync(x => true);
            await ctx.RespondAsync(message.Result.Content);

            /* 
               
            await ctx.RespondAsync("Mahal ba ni Gian ang kanyang joa?");
            var results = await ctx.Message.GetNextMessageAsync(m =>
            {
                return m.Content.ToLower() == "oo";
            });

            if (!results.TimedOut) await ctx.RespondAsync("Mali ka doon lodi."); 
            
            */
        }
    }
}