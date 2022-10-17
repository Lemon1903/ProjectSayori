using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using ProjectSayori.Commands.DanbooruApi;


namespace ProjectSayori.Commands.QuoteGenerator
{
        public class SearchQuoteCommand : BaseCommandModule
    {
        [Command("search-quote")]
        public async Task SearchQuote(CommandContext ctx, [RemainingText]string query)
        {
            ApiHelper.InitializeClient();
            var quote = await SearchQuoteProcessor.SearchQuote(query);
            var builder = BuildRandomQuoteEmbed(quote);
            await ctx.Channel.SendMessageAsync(builder).ConfigureAwait(false);
        }

        private DiscordEmbedBuilder BuildRandomQuoteEmbed(RandomQuoteModel quote)
        {
            var builder = new DiscordEmbedBuilder() { Color = DiscordColor.Green };

            if (quote != null)
            {
                builder.Title = "Here's a random quote related to your query.";
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