using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using ProjectSayori.Commands.DanbooruApi;


namespace ProjectSayori.Commands.QuoteGenerator
{
    public class RandomQuoteCommand : BaseCommandModule
    {

        [Command("random-quote")]
        public async Task RandomQuote(CommandContext ctx)
        {
            ApiHelper.InitializeClient();
            var quote = await RandomQuoteProcessor.LoadRandomQuote();
            var builder = BuildRandomQuoteEmbed(quote);
            await ctx.Channel.SendMessageAsync(builder).ConfigureAwait(false);
        }

        [Command("random-quote")]
        public async Task RandomQuote(CommandContext ctx, [RemainingText]string tag)
        {
            ApiHelper.InitializeClient();
            var quote = await RandomQuoteProcessor.LoadRandomQuote(tag.Replace(" ", ""));
            var builder = BuildRandomQuoteEmbed(quote);
            await ctx.Channel.SendMessageAsync(builder).ConfigureAwait(false);
        }

        private DiscordEmbedBuilder BuildRandomQuoteEmbed(RandomQuoteModel quote)
        {
            var builder = new DiscordEmbedBuilder() { Color = DiscordColor.Green };

            if (quote != null)
            {
                builder.Title = "Here's a random quote for you.";
                builder.Footer = new DiscordEmbedBuilder.EmbedFooter { Text = $"- {quote.Author}" };
                builder.AddField("Tags:", string.Join(", ", quote.Tags));
                builder.AddField("Quote:", quote.Content);
            }
            else
            {
                builder.Title = "Sorry, could not find any matching quotes.";
            }

            return builder;
        }
    }
}