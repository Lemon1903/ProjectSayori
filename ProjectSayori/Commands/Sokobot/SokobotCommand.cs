using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Sokobot;


public class SokobotCommand : BaseCommandModule
{
    private Game game;

    [Command("sokobot")]
    public async Task Sokobot(CommandContext ctx)
    {
        if (GameManager.Games.ContainsKey(ctx.User))
            game = GameManager.LoadGame(ctx.User);
        else
            game = GameManager.NewGame(ctx.User, 1);
        
        game.Update(ctx.Client, String.Empty);
        game.GameMessage = await ctx.RespondAsync(game.MessageBuilder).ConfigureAwait(false);
    }

    [Command("continue")]
    public async Task Continue(CommandContext ctx)
    {
        game = GameManager.LoadGame(ctx.User);
        if (game.IsWon)
        {
            await ctx.Channel.DeleteMessagesAsync(new DiscordMessage[] { game.GameMessage, game.WonMessage });
            game = GameManager.NewGame(ctx.User, game.Level + 1);
            game.Update(ctx.Client, String.Empty);
            game.GameMessage = await ctx.RespondAsync(game.MessageBuilder).ConfigureAwait(false);
        }
    }
}