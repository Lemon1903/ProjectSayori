using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using System.Numerics;


namespace Sokobot
{
    public class Game
    {
        public DiscordMessage GameMessage { get; set; }
        public DiscordMessage WonMessage { get; set; }
        public DiscordMessageBuilder MessageBuilder { get; private set; }
        public bool IsWon { get; private set; }
        public bool HasMessaged { get; set; }
        public int Level { get; }
        private DiscordUser currentUser;
        private Grid grid;

        public Game(DiscordUser user, int level)
        {
            grid = new Grid();
            Level = level;
            currentUser = user;
        }

        public void Reset()
        {
            grid.Reset();
        }

        public void Update(DiscordClient client, string move)
        {
            if (!IsWon)
            {
                MovePlayer(move);
                grid.UpdateGrid();
                UpdateGameEmbed(client);

                if (grid.CratesLeft <= 0)
                    IsWon = true;
            }
        }

        public DiscordMessageBuilder SetWonEmbed(DiscordClient client)
        {
            var emBuilder = new DiscordEmbedBuilder{
                Title = $"Congratulations, you win {currentUser.Username}!",
                Description = $"Type ``!continue`` to continue to the next level.",
            };

            return new DiscordMessageBuilder().WithEmbed(emBuilder);
        }

        private void MovePlayer(string move)
        {
            switch(move)
            {
                case "move_left":
                    grid.CurrentPlayer.Move(new Vector2(0, -1));
                    break;
                case "move_right":
                    grid.CurrentPlayer.Move(new Vector2(0, 1));
                    break;
                case "move_up":
                    grid.CurrentPlayer.Move(new Vector2(-1, 0));
                    break;
                case "move_down":
                    grid.CurrentPlayer.Move(new Vector2(1, 0));
                    break;
            }
        }

        private void UpdateGameEmbed(DiscordClient client)
        {
            var emBuilder = new DiscordEmbedBuilder{
                Title = $"Level {Level}\t\tPlayer: {currentUser.Username}",
                Color = DiscordColor.Azure,
                Description = grid.ConvertToString(),
                Footer = new DiscordEmbedBuilder.EmbedFooter{ Text = $"Number of crates left: {grid.CratesLeft}" },
            };

            MessageBuilder = new DiscordMessageBuilder()
                .WithEmbed(emBuilder)
                .AddComponents(new DiscordComponent[] {
                    CreateButton(client, "move_left", null, ":arrow_left:"),
                    CreateButton(client, "move_right", null, ":arrow_right:"),
                    CreateButton(client, "move_up", null, ":arrow_up:"),
                    CreateButton(client, "move_down", null, ":arrow_down:"),
                    CreateButton(client, "retry", null, ":arrows_counterclockwise:"),
                });
        }

        private DiscordButtonComponent CreateButton(DiscordClient client, string id, string? buttonName, string? emojiName)
        {
            DiscordButtonComponent? button = null;

            if (buttonName != null)
            {
                button = new DiscordButtonComponent(ButtonStyle.Primary, id, buttonName);
            }

            if (emojiName != null)
            {
                var emoji = new DiscordComponentEmoji(DiscordEmoji.FromName(client, emojiName));
                button = new DiscordButtonComponent(ButtonStyle.Primary, id, null, false, emoji);
            }

            return button;
        }
    }
}