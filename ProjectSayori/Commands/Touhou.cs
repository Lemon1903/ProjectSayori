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
    public class Touhou : BaseCommandModule
    {
        #region "Player datas"

        double P1Power = 1.00;
        double P2Power = 1.00;

        double P1HP = 500;
        double P2HP = 500;

        double P1Graze = 1.80;
        double P2Graze = 1.80;

        int P1Dmg = 1;
        int P2Dmg = 1;

        bool P1Spread = false;
        bool P1Focus = false;
        bool P1GrazeOn = false;
        bool P1flag = false;

        bool P2Spread = false;
        bool P2Focus = false;
        bool P2GrazeOn = false;
        bool P2flag = false;

        #endregion

        [Command("bullets")]
        [Description("A game heavily inspired from Touhou: Lost Word! Shoot your opponents!")]
        public async Task Game(CommandContext ctx, [Description("Name of Player 1")]string Pl1,
                                                   [Description("Name of Player 2")]string Pl2)
        {
            if (Pl1 == null | Pl2 == null)
            {
                await ctx.RespondAsync("```?bullets [name1] [name2]```");
            }
            Restart();
            var interactivity = ctx.Client.GetInteractivity();

        #region Turn Restart

            TurnRestart:
            P1Spread = false;
            P1Focus = false;
            P1GrazeOn = false;
            P1Graze = P1Graze + 0.20;
            P1Graze = Math.Round(P1Graze, 1);
            P2Graze = P2Graze + 0.20;
            P2Graze = Math.Round(P2Graze, 1);
            P2Spread = false;
            P2Focus = false;
            P2GrazeOn = false;
            P1Dmg = 1;
            P2Dmg = 1;

        #endregion

        #region "Player One Commands"
        P1restart:

            #region "Player One Embed Display"
            // Information of the Embed
            var PlayerOne = new DiscordEmbedBuilder
            {
                Title = $"{Pl1}'s Turn!",
                Color = DiscordColor.Green
            };
            PlayerOne.AddField("Health Points", P1HP.ToString());
            PlayerOne.AddField("Power Points", P1Power.ToString());
            PlayerOne.AddField("Graze Points", P1Graze.ToString());

            // Display Embed
            var P1message = await ctx.Channel.SendMessageAsync(embed: PlayerOne).ConfigureAwait(false);
            
            // Call Emojis as Variables
            var P1spreadShot = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_s:");
            var P1focusShot = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_f:");
            var P1graze = DiscordEmoji.FromName(ctx.Client, ":shield:");
            var P1usePower = DiscordEmoji.FromName(ctx.Client, ":fire:");
            var P1help = DiscordEmoji.FromName(ctx.Client, ":question:");
            var P1forfeit = DiscordEmoji.FromName(ctx.Client, ":flag_white:");

            // Add Emojis to Embed
            await P1message.CreateReactionAsync(P1spreadShot).ConfigureAwait(false);
            await P1message.CreateReactionAsync(P1focusShot).ConfigureAwait(false);
            await P1message.CreateReactionAsync(P1graze).ConfigureAwait(false);
            await P1message.CreateReactionAsync(P1usePower).ConfigureAwait(false);
            await P1message.CreateReactionAsync(P1help).ConfigureAwait(false);
            await P1message.CreateReactionAsync(P1forfeit).ConfigureAwait(false);

            await Task.Delay(500);

            var P1Choose = await interactivity.WaitForReactionAsync(x => true).ConfigureAwait(false);
            #endregion 


            if (P1Choose.Result.Emoji == P1spreadShot)
            {
                await ctx.RespondAsync("Player 1 readies his Spread Shot!");
                P1Spread = true;
            }
            else if (P1Choose.Result.Emoji == P1focusShot)
            {
                await ctx.RespondAsync("Player 1 readies his Focus Shot!");
                P1Focus = true;
            }
            else if (P1Choose.Result.Emoji == P1graze)
            {
                if (P1Graze < 1)
                {
                    await ctx.RespondAsync("You don't have enough Graze Points to use!");
                    goto P1restart;
                }
                else if (P1GrazeOn == true)
                {
                    await ctx.RespondAsync("You cannot use another Graze Power!");
                    goto P1restart;
                }
                else
                {
                    await ctx.RespondAsync("Player 1 used one Graze Power!");
                    P1Graze = P1Graze - 1;
                    P1GrazeOn = true;
                    goto P1restart;
                }
            }
            else if (P1Choose.Result.Emoji == P1usePower)
            {
                if (P1Power < 1)
                {
                    await ctx.RespondAsync("You don't have enough Power Points.");
                    goto P1restart;
                }
                else
                {
                    await ctx.RespondAsync("Player 1 uses one Power Point! ");
                    P1Dmg = P1Dmg * 2;
                    P1Power = P1Power - 1;
                    goto P1restart;
                }
            }
            else if (P1Choose.Result.Emoji == P1help)
            {
                await ctx.RespondAsync("```Spread Shot (S) = Deals low damage but rewards high Power Points.\n" +
                    "Focus Shot (F) = Deals high damage but rewards low Power Points.\n" +
                    "Graze (Shield) = Grazes bullets. You gain one Graze Point for every five turns.\n" +
                    "Power (Fire) = Doubles your Damage Multiplier by using one Power Point for this turn.\n\n" +
                    "Damage Calculation:\n" +
                    "(Shot Type)*(Damage Multiplier)*(Power Points)```");
                goto P1restart;
            }
            else if (P1Choose.Result.Emoji == P1forfeit)
            {
                P1flag = true;
            }

        #endregion

        #region "Player Two Commands"
        P2restart:

            #region "Player Two Embed Display"
            // Information of the Embed
            var PlayerTwo = new DiscordEmbedBuilder
            {
                Title = $"{Pl2}'s Turn!",
                Color = DiscordColor.Green
            };
            PlayerTwo.AddField("Health Points", P2HP.ToString());
            PlayerTwo.AddField("Power Points", P2Power.ToString());
            PlayerTwo.AddField("Graze Points", P2Graze.ToString());

            // Display Embed
            var P2message = await ctx.Channel.SendMessageAsync(embed: PlayerTwo).ConfigureAwait(false);

            // Call Emojis as Variables
            var P2spreadShot = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_s:");
            var P2focusShot = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_f:");
            var P2graze = DiscordEmoji.FromName(ctx.Client, ":shield:");
            var P2usePower = DiscordEmoji.FromName(ctx.Client, ":fire:");
            var P2help = DiscordEmoji.FromName(ctx.Client, ":question:");
            var P2forfeit = DiscordEmoji.FromName(ctx.Client, ":flag_white:");

            // Add Emojis to embed
            await P2message.CreateReactionAsync(P2spreadShot).ConfigureAwait(false);
            await P2message.CreateReactionAsync(P2focusShot).ConfigureAwait(false);
            await P2message.CreateReactionAsync(P2graze).ConfigureAwait(false);
            await P2message.CreateReactionAsync(P2usePower).ConfigureAwait(false);
            await P2message.CreateReactionAsync(P2help).ConfigureAwait(false);
            await P2message.CreateReactionAsync(P2forfeit).ConfigureAwait(false);

            await Task.Delay(500);

            var P2Choose = await interactivity.WaitForReactionAsync(x => true).ConfigureAwait(false);
            #endregion


            if (P2Choose.Result.Emoji == P2spreadShot)
            {
                await ctx.RespondAsync("Player 2 readies his Spread Shot!");
                P2Spread = true;
            }
            else if (P2Choose.Result.Emoji == P2focusShot)
            {
                await ctx.RespondAsync("Player 2 readies his Focus Shot!");
                P2Focus = true;
            }
            else if (P2Choose.Result.Emoji == P2graze)
            {
                if (P2Graze < 1)
                {
                    await ctx.RespondAsync("You don't have enough Graze Points to use!");
                    goto P2restart;
                }
                else if (P2GrazeOn == true)
                {
                    await ctx.RespondAsync("You cannot use another Graze Power!");
                    goto P2restart;
                }
                else
                {
                    await ctx.RespondAsync("Player 2 used one Graze Power!");
                    P2Graze = P2Graze - 1;
                    P2GrazeOn = true;
                    goto P2restart;
                }
            }
            else if (P2Choose.Result.Emoji == P2usePower)
            {
                if (P2Power < 1)
                {
                    await ctx.RespondAsync("You don't have enough Power Points.");
                    goto P2restart;
                }
                else
                {
                    await ctx.RespondAsync("Player 2 uses one Power Point! ");
                    P2Dmg = P2Dmg * 2;
                    P2Power = P2Power - 1;
                    goto P2restart;
                }
            }
            else if (P2Choose.Result.Emoji == P2help)
            {
                await ctx.RespondAsync("```Spread Shot (S) = Deals low damage but rewards high Power Points.\n" +
                    "Focus Shot (F) = Deals high damage but rewards low Power Points.\n" +
                    "Graze (Shield) = Grazes bullets. You gain one Graze Point for every five turns.\n" +
                    "Power (Fire) = Doubles your Damage Multiplier by using one Power Point for this turn.\n\n" +
                    "Damage Calculation:\n" +
                    "(Shot Type)*(Damage Multiplier)*(Power Points)```");
                goto P2restart;
            }
            else if (P2Choose.Result.Emoji == P2forfeit)
            {
                P2flag = true;
            }
            #endregion

        #region "Battle Results"



        double P1DmgDealt = 0;
        double P2DmgDealt = 0;
        double P1PwrGained = 0;
        double P2PwrGained = 0;

        if (P2GrazeOn == true)
        {
            var PlayerOneResults = new DiscordEmbedBuilder
            {
                Title = $"{Pl1} dealt no damage due to {Pl2}'s Graze Power!",
                Color = DiscordColor.Red
            };
            PlayerOneResults.AddField("Damage Dealt", P1DmgDealt.ToString());
            PlayerOneResults.AddField("Power Gained", P1PwrGained.ToString());

            var P1R = await ctx.Channel.SendMessageAsync(embed: PlayerOneResults).ConfigureAwait(false);
        }
        else if (P1Spread == true)
        {
            if (P1Power <= 1)
            {
                P1DmgDealt = 15 * P1Dmg;
            }
            else
            {
                P1DmgDealt = 15 * P1Dmg * P1Power;
            }
            P1PwrGained = P1DmgDealt/50;
            P1Power = P1Power + P1PwrGained;

            P1DmgDealt = Math.Round(P1DmgDealt, 1);
            P1PwrGained = Math.Round(P1PwrGained, 2);
            P1Power = Math.Round(P1Power, 2);

            #region P1Results
            var PlayerOneResults = new DiscordEmbedBuilder
            {
                Title = $"{Pl1} uses Spread Shot!",
                Color = DiscordColor.Red
            };
            PlayerOneResults.AddField("Damage Dealt", P1DmgDealt.ToString());
            PlayerOneResults.AddField("Power Gained", P1PwrGained.ToString());

            var P1R = await ctx.Channel.SendMessageAsync(embed: PlayerOneResults).ConfigureAwait(false);
            #endregion

            P2HP = P2HP - P1DmgDealt;
            P2HP = Math.Round(P2HP, 1);
        }
        else if (P1Focus == true)
        {
            if (P1Power <= 1)
            {
                P1DmgDealt = 60 * P1Dmg;
            }
            else
            {
                P1DmgDealt = 60 * P1Dmg * P1Power;
            }
                P1PwrGained = P1DmgDealt / 1000;
            P1Power = P1Power + P1PwrGained;
            P1DmgDealt = Math.Round(P1DmgDealt, 1);
            P1PwrGained = Math.Round(P1PwrGained, 2);
            P1Power = Math.Round(P1Power, 2);

            #region P1Results

            var PlayerOneResults = new DiscordEmbedBuilder
            {
                Title = $"{Pl1} uses Focus Shot!",
                Color = DiscordColor.Red
            };
            PlayerOneResults.AddField("Damage Dealt", P1DmgDealt.ToString());
            PlayerOneResults.AddField("Power Gained", P1PwrGained.ToString());

            var P1R = await ctx.Channel.SendMessageAsync(embed: PlayerOneResults).ConfigureAwait(false);
            #endregion

            P2HP = P2HP - P1DmgDealt;
            P2HP = Math.Round(P2HP, 1);
        }

        if (P1GrazeOn == true)
        {
            var PlayerTwoResults = new DiscordEmbedBuilder
            {
                Title = $"{Pl2} dealt no damage due to {Pl1}'s Graze Power!",
                Color = DiscordColor.Red
            };
            PlayerTwoResults.AddField("Damage Dealt", P2DmgDealt.ToString());
            PlayerTwoResults.AddField("Power Gained", P2PwrGained.ToString());

            var P2R = await ctx.Channel.SendMessageAsync(embed: PlayerTwoResults).ConfigureAwait(false);
        }
        else if (P2Spread == true)
        {
            if (P2Power <= 1)
            {
                P2DmgDealt = 15 * P2Dmg;
            }
            else
            {
                P2DmgDealt = 15 * P2Dmg * P2Power;
            }
            P2PwrGained = P2DmgDealt / 50;
            P2Power = P2Power + P2PwrGained;

            P2DmgDealt = Math.Round(P2DmgDealt, 1);
            P2PwrGained = Math.Round(P2PwrGained, 2);
            P2Power = Math.Round(P2Power, 2);

            #region P2Results

            var PlayerTwoResults = new DiscordEmbedBuilder
            {
                Title = $"{Pl2} uses Spread Shot!",
                Color = DiscordColor.Red
            };
            PlayerTwoResults.AddField("Damage Dealt", P2DmgDealt.ToString());
            PlayerTwoResults.AddField("Power Gained", P2PwrGained.ToString());

            var P2R = await ctx.Channel.SendMessageAsync(embed: PlayerTwoResults).ConfigureAwait(false);
            #endregion

            P1HP = P1HP - P2DmgDealt;
            P1HP = Math.Round(P1HP, 1);
        }
        else if (P2Focus == true)
        {
            if (P2Power <= 1)
            {
                P2DmgDealt = 60 * P2Dmg;
            }
            else
            {
                P2DmgDealt = 60 * P2Dmg * P2Power;
            }
                P2PwrGained = P2DmgDealt / 1000;
            P2Power = P2Power + P2PwrGained;
            P2DmgDealt = Math.Round(P2DmgDealt, 1);
            P2PwrGained = Math.Round(P2PwrGained, 2);
            P2Power = Math.Round(P2Power, 2);

            #region P2Results
            var PlayerTwoResults = new DiscordEmbedBuilder
            {
                Title = $"{Pl2} uses Focus Shot!",
                Color = DiscordColor.Red
            };
            PlayerTwoResults.AddField("Damage Dealt", P2DmgDealt.ToString());
            PlayerTwoResults.AddField("Power Gained", P2PwrGained.ToString());

            var P2R = await ctx.Channel.SendMessageAsync(embed: PlayerTwoResults).ConfigureAwait(false);
            #endregion

            P1HP = P1HP - P2DmgDealt;
            P1HP = Math.Round(P1HP, 1);
        }


        if (P1HP < 0 & P2HP < 0)
        {
            var Finished = new DiscordEmbedBuilder
            {
                Title = $"Draw!",
                Color = DiscordColor.Orange
            };

            var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
        }
        else if (P1HP < 0)
        {
            var Finished = new DiscordEmbedBuilder
            {
                Title = $"{Pl1} loses all his health points. {Pl2} wins!",
                Color = DiscordColor.Orange
            };

            var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
        }
        else if (P2HP < 0)
        {
            var Finished = new DiscordEmbedBuilder
            {
                Title = $"{Pl2} loses all his health points. {Pl1} wins!",
                Color = DiscordColor.Orange
            };

            var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
        }
        else if (P1flag == true & P2flag == true)
            {
                if (P1HP < P2HP)
                {
                    var Finished = new DiscordEmbedBuilder
                    {
                        Title = $"Both forfeited, but {Pl1} has low Health Points than {Pl2}. {Pl2} wins!",
                        Color = DiscordColor.Orange
                    };

                    var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
                }
                else if (P2HP < P1HP)
                {
                    var Finished = new DiscordEmbedBuilder
                    {
                        Title = $"Both forfeited, but {Pl2} has low Health Points than {Pl1}. {Pl1} wins!",
                        Color = DiscordColor.Orange
                    };

                    var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
                }
                else if (P1HP == P2HP)
                {
                    var Finished = new DiscordEmbedBuilder
                    {
                        Title = $"Both forfeited. No one wins.",
                        Color = DiscordColor.Orange
                    };

                    var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
                }
            }
        else if (P1flag == true & P2flag == false)
            {
                var Finished = new DiscordEmbedBuilder
                {
                    Title = $"{Pl1} forfeited. {Pl2} wins!",
                    Color = DiscordColor.Orange
                };

                var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
            }
        else if (P1flag == false & P2flag == true)
            {
                var Finished = new DiscordEmbedBuilder
                {
                    Title = $"{Pl2} forfeited. {Pl1} wins!",
                    Color = DiscordColor.Orange
                };

                var Win = await ctx.Channel.SendMessageAsync(embed: Finished).ConfigureAwait(false);
            }
        else
        {
            goto TurnRestart;
        }
        }
        #endregion
        
        
        public void Restart()
        {
            P1Power = 1.00;
            P2Power = 1.00;

            P1HP = 500;
            P2HP = 500;

            P1Graze = 1.80;
            P2Graze = 1.80;

            P1Dmg = 1;
            P2Dmg = 1;

            P1Spread = false;
            P1Focus = false;
            P1GrazeOn = false;
            P1flag = false;

            P2Spread = false;
            P2Focus = false;
            P2GrazeOn = false;
            P2flag = false;
        }
    }
}