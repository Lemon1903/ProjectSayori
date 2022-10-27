using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;


namespace Sokobot
{
    public class ButtonEvent
    {
        public static async Task ButtonPressed(DiscordClient s, ComponentInteractionCreateEventArgs e)
        {
            var gamePlayer = e.Message.Embeds[0].Title.Split(':')[1].Trim();
            if (e.User.Username != gamePlayer)
            {
                await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                await e.Channel.SendMessageAsync(new DiscordMessageBuilder()
                    .WithContent($"Play your game {e.User.Mention}!")
                    .WithAllowedMention(new UserMention(e.User)));
            }
            else
            {
                Game game = GameManager.LoadGame(e.User);
                if (e.Id == "retry")
                    game.Reset();
                
                game.Update(s, e.Id);
                await e.Interaction.CreateResponseAsync(
                    InteractionResponseType.UpdateMessage,
                    new DiscordInteractionResponseBuilder(game.MessageBuilder));

                if (game.IsWon && !game.HasMessaged)
                {
                    game.WonMessage = await e.Channel.SendMessageAsync(game.SetWonEmbed(s));
                    game.HasMessaged = true;
                }
            }
        }
    }
}