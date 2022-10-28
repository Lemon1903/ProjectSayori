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
            // check whether it's the player who clicked one of the buttons
            var gamePlayer = e.Message.Embeds[0].Title.Split(':')[1].Trim();
            if (e.User.Username != gamePlayer)
            {
                var emBuilder = new DiscordEmbedBuilder{
                    Title = $"Play your own game {e.User.Username}!",
                    Description = "Type ``!sokobot`` to load or create your own game.",
                };

                await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                await e.Channel.SendMessageAsync(new DiscordMessageBuilder().WithEmbed(emBuilder));
            }
            else
            {
                Game game = GameManager.LoadGame(e.User);
                if (e.Id == "retry")    // checks the id of retry button
                    game.Reset();
                
                game.Update(s, e.Id);
                await e.Interaction.CreateResponseAsync(
                    InteractionResponseType.UpdateMessage,
                    new DiscordInteractionResponseBuilder(game.MessageBuilder));

                if (game.IsWon && !game.HasMessaged)
                {
                    game.WonMessage = await e.Channel.SendMessageAsync(game.SetWonEmbed());
                    game.HasMessaged = true;
                }
            }
        }
    }
}