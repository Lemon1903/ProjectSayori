using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace ProjectSayori.Commands
{
    public class ScheduleCopy : BaseCommandModule
    {
        private static readonly string[] subjects =
        {
            /*0*/ "Computer Programming 1",
            /*1*/ "Physical Fitness and Self-Testing Activities",
            /*2*/ "Filipinolohiya at Pambansang Kaunlaran",
            /*3*/ "Purposive Communication",
            /*4*/ "Politics, Governance and Citizenship",
            /*5*/ "Introduction to Computing",
            /*6*/ "Mathematics in the Modern World",
            /*7*/ "Civic Welfare Training Service 1"
        };
        private readonly Dictionary<string, string[,]> subjectFields = new()
        {
            { "Monday", new string[,] {
                { subjects[0], "09:00 - 12:00" },
                { subjects[1], "12:00 - 14:00" },
                { "(No Subject)", "14:00 - 15:00" },
                { subjects[2], "15:00 - 16:30" },
                { "(No Subject)", "16:30 - 19:30" },
                { subjects[3], "19:30 - 21:00" },
            }},
            
            { "Tuesday", new string[,] {
                { subjects[4], "09:00 - 10:30" },
                { subjects[5], "10:30 - 13:30" },
                { "(No Subject)", "13:30 - 15:00" },
                { subjects[6], "15:00 - 16:30" },
            }},

            { "Thursday", new string[,] {
                { subjects[0], "09:00 - 11:00" },
                { "(No Subject)", "11:00 - 15:00" },
                { subjects[2], "15:00 - 16:30" },
                { "(No Subject)", "16:30 - 19:30" },
                { subjects[3], "19:30 - 21:00" },
            }},

            { "Friday", new string[,] {
                { subjects[4], "09:00 - 10:30" },
                { "(No Subject)", "10:30 - 11:30" },
                { subjects[5], "11:30 - 15:30" },
                { "(No Subject)", "13:30 - 15:00" },
                { subjects[6], "15:00 - 16:30" },
            }},

            { "Sunday", new string[,] {
                { subjects[7], "08:00 - 17:00" },
            }},
        };

        [Command("schedule-copy"), Description("Tells what class you must be in right now.")]
        public async Task Schedule(CommandContext ctx)
        {
            DateTime now = DateTime.Now;
            string dayToday = now.DayOfWeek.ToString();
            var builder = new DiscordEmbedBuilder{ Title = $"Today is {dayToday}" };

            if (subjectFields.ContainsKey(dayToday))
            {
                for (int i = 0; i < subjectFields[dayToday].GetLength(0); i++)
                {
                    builder.Color = DiscordColor.Red;
                    builder.AddField(subjectFields[dayToday][i, 0], subjectFields[dayToday][i, 1]);
                }
            }
            else
            {
                builder.Color = DiscordColor.Green;
                builder.Description = "There will be no classes for today!";
            }

            await ctx.Channel.SendMessageAsync(builder);
        }


        [Command("schedule-copy")]
        public async Task Schedule(CommandContext ctx, [RemainingText]string input)
        {
            

            await ctx.Channel.SendMessageAsync("Hello!");
        }



/* ======================================================================================================================*/
        public async Task Class(CommandContext ctx, [RemainingText] string x)
        {
            DateTime now = DateTime.Now;
            #region First if
            if (x == null)
            {
                if (now.DayOfWeek.ToString() == "Monday")
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Today is Monday!",
                        Color = DiscordColor.Red
                    };
                    builder.AddField(subjects[0], "09:00 - 12:00");
                    builder.AddField(subjects[1], "12:00 - 14:00");
                    builder.AddField("(No Subject)", "14:00 - 15:00");
                    builder.AddField(subjects[2], "15:00 - 16:30");
                    builder.AddField("(No Subject)", "16:30 - 19:30");
                    builder.AddField(subjects[3], "19:30 - 21:00");

                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
                else if (now.DayOfWeek.ToString() == "Tuesday")
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Today is Tuesday!",
                        Color = DiscordColor.Red
                    };
                    builder.AddField(subjects[4], "09:00 - 10:30");
                    builder.AddField(subjects[5], "10:30 - 13:30");
                    builder.AddField("(No Subject)", "13:30 - 15:00");
                    builder.AddField(subjects[6], "15:00 - 16:30");

                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
                else if (now.DayOfWeek.ToString() == "Thursday")
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Today is Thursday!",
                        Color = DiscordColor.Red
                    };
                    builder.AddField(subjects[0], "09:00 - 11:00");
                    builder.AddField("(No Subject)", "11:00 - 15:00");
                    builder.AddField(subjects[2], "15:00 - 16:30");
                    builder.AddField("(No Subject)", "16:30 - 19:30");
                    builder.AddField(subjects[3], "19:30 - 21:00");

                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
                else if (now.DayOfWeek.ToString() == "Friday")
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Today is Friday!",
                        Color = DiscordColor.Red
                    };
                    builder.AddField(subjects[4], "09:00 - 10:30");
                    builder.AddField("(No Subject)", "10:30 - 11:30");
                    builder.AddField(subjects[5], "11:30 - 15:30");
                    builder.AddField("(No Subject)", "13:30 - 15:00");
                    builder.AddField($"~~{subjects[6]}~~", "15:00 - 16:30");

                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
                else if (now.DayOfWeek.ToString() == "Sunday")
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Today is Sunday!",
                        Color = DiscordColor.Red
                    };
                    builder.AddField(subjects[7], "08:00 - 17:00");

                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
                else
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = $"Today is {now.DayOfWeek}!",
                        Color = DiscordColor.Green,
                        Description = "There will be no classes for today!"
                    };

                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
            }
            #endregion
            #region Second if
            else if (x.ToLower() == "subject")
            {
                switch (now.DayOfWeek.ToString())
                {
                    case "Monday":
                        if (now.Hour < 9)
                        {
                            await ctx.RespondAsync("There is no class yet. Wait 'till 9.");
                        }
                        else if (now.Hour >= 9 & now.Hour < 12)
                        {
                            await ctx.RespondAsync($"{subjects[0]}\nPermalink: https://us02web.zoom.us/" +
                                $"j/3205431145?pwd=UWlBb0JiVjJPZmsyem5wVlMrTS82QT09");
                        }
                        else if (now.Hour >= 12 & now.Hour < 14)
                        {
                            await ctx.RespondAsync(subjects[1]);
                        }
                        else if (now.Hour >= 15 & ((now.Hour <= 16 & now.Minute < 30) | now.Hour < 16))
                        {
                            await ctx.RespondAsync($"{subjects[2]}\nPermalink: https://meet.google.com/vdv-nyjy-wng");
                        }
                        else if ((now.Hour > 19 | (now.Hour >= 19 & now.Minute >= 30)) & ((now.Hour <= 21 & now.Minute < 30) | now.Hour < 21))
                        {
                            await ctx.RespondAsync(subjects[3]);
                        }
                        else
                        {
                            await ctx.RespondAsync("No current subject.");
                        }
                        break;
                    case "Tuesday":
                        if (now.Hour < 9)
                        {
                            await ctx.RespondAsync("There is no class yet. Wait 'till 9.");
                        }
                        else if (now.Hour >= 9 & ((now.Hour <= 10 & now.Minute < 30) | now.Hour < 10))
                        {
                            await ctx.RespondAsync($"{subjects[4]}\nPermalink: https://meet.google.com/twn-czpk-ezw");
                        }
                        else if ((now.Hour > 10 | (now.Hour >= 10 & now.Minute >= 30)) & ((now.Hour <= 13 & now.Minute < 30) | now.Hour < 13))
                        {
                            await ctx.RespondAsync(subjects[5]);
                        }
                        else if (now.Hour >= 15 & ((now.Hour <= 16 & now.Minute < 30) | now.Hour < 16))
                        {
                            await ctx.RespondAsync($"{subjects[6]}\n Permalink: https://us02web.zoom.us/j/" +
                                $"81384059714?pwd=WEt1NmZTcUIzWUkrajZvQ1N6OHRHZz09");
                        }
                        else
                        {
                            await ctx.RespondAsync("No current subject.");
                        }
                        break;
                    case "Thursday":
                        if (now.Hour < 9)
                        {
                            await ctx.RespondAsync("There is no class yet. Wait 'till 9.");
                        }
                        else if (now.Hour >= 9 & now.Hour < 11)
                        {
                            await ctx.RespondAsync($"{subjects[0]}\nPermalink: https://us02web.zoom.us/" +
                                $"j/3205431145?pwd=UWlBb0JiVjJPZmsyem5wVlMrTS82QT09");
                        }
                        else if (now.Hour >= 15 & ((now.Hour <= 16 & now.Minute < 30) | now.Hour < 16))
                        {
                            await ctx.RespondAsync($"{subjects[2]}\nPermalink: https://meet.google.com/vdv-nyjy-wng");
                        }
                        else if ((now.Hour > 19 | (now.Hour >= 19 & now.Minute >= 30)) & ((now.Hour <= 21 & now.Minute < 30) | now.Hour < 21))
                        {
                            await ctx.RespondAsync(subjects[3]);
                        }
                        else
                        {
                            await ctx.RespondAsync("No current subject.");
                        }
                        break;
                    case "Friday":
                        if (now.Hour < 9)
                        {
                            await ctx.RespondAsync("There is no class yet. Wait 'till 9.");
                        }
                        else if (now.Hour >= 9 & ((now.Hour <= 10 & now.Minute < 30) | now.Hour < 10))
                        {
                            await ctx.RespondAsync($"subjects[4]\nPermalink: https://meet.google.com/twn-czpk-ezw");
                        }
                        else if ((now.Hour > 11 | (now.Hour >= 11 & now.Minute >= 30)) & ((now.Hour <= 13 & now.Minute < 30) | now.Hour < 13))
                        {
                            await ctx.RespondAsync(subjects[5]);
                        }
                        else if (now.Hour >= 15 & ((now.Hour <= 16 & now.Minute < 30) | now.Hour < 16))
                        {
                            await ctx.RespondAsync(subjects[6] + "\n(Asynchronous)");
                        }
                        else
                        {
                            await ctx.RespondAsync("No current subject.");
                        }
                        break;
                    case "Sunday":
                        if (now.Hour < 8)
                        {
                            await ctx.RespondAsync("There is no class yet. Wait 'till 8.");
                        }
                        else if (now.Hour >= 8 & now.Hour < 17)
                        {
                            await ctx.RespondAsync(subjects[7]);
                        }
                        else
                        {
                            await ctx.RespondAsync("No current subject.");
                        }
                        break;
                    default:
                        await ctx.RespondAsync("There is no class today!");
                        break;
                }
            }
            #endregion
            #region else if (dates)
            else if (x.ToLower() == "monday")
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = "Monday Schedule",
                    Color = DiscordColor.Orange
                };
                builder.AddField(subjects[0], "09:00 - 12:00");
                builder.AddField(subjects[1], "12:00 - 14:00");
                builder.AddField("(No Subject)", "14:00 - 15:00");
                builder.AddField(subjects[2], "15:00 - 16:30");
                builder.AddField("(No Subject)", "16:30 - 19:30");
                builder.AddField(subjects[3], "19:30 - 21:00");

                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower() == "tuesday")
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = "Tuesday Schedule",
                    Color = DiscordColor.Orange
                };
                builder.AddField(subjects[4], "09:00 - 10:30");
                builder.AddField(subjects[5], "10:30 - 13:30");
                builder.AddField("(No Subject)", "13:30 - 15:00");
                builder.AddField(subjects[6], "15:00 - 16:30");

                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower() == "wednesday" | x.ToLower() == "saturday")
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"Wednesday and Saturday Schedule",
                    Color = DiscordColor.Green,
                    Description = "There will be no classes for that day!"
                };
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower() == "thursday")
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = "Thursday Schedule",
                    Color = DiscordColor.Orange
                };
                builder.AddField(subjects[0], "09:00 - 11:00");
                builder.AddField("(No Subject)", "11:00 - 15:00");
                builder.AddField(subjects[2], "15:00 - 16:30");
                builder.AddField("(No Subject)", "16:30 - 19:30");
                builder.AddField(subjects[3], "19:30 - 21:00");

                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower() == "friday")
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = "Friday Schedule",
                    Color = DiscordColor.Orange
                };
                builder.AddField(subjects[4], "09:00 - 10:30");
                builder.AddField("(No Subject)", "10:30 - 11:30");
                builder.AddField(subjects[5], "11:30 - 13:30");
                builder.AddField("(No Subject)", "13:30 - 15:00");
                builder.AddField($"~~{subjects[6]}~~", "15:00 - 16:30");

                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower() == "sunday")
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = "Sunday Schedule",
                    Color = DiscordColor.Orange
                };
                builder.AddField(subjects[7], "08:00 - 17:00");

                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            #endregion
            else if (x.ToLower() == "date")
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"Today is {now.DayOfWeek}, {now}",
                    Color = DiscordColor.Green,
                    Description = "I don't know why I put this, but it may be useful."
                };
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            #region else if (subject search)
            else if (x.ToLower().Contains("computer") | x.ToLower().Contains("program"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[0], "Monday: 09:00 - 12:00");
                builder.AddField(subjects[0], "Thursday: 09:00 - 11:00");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower().Contains("physical"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[1], "Monday: 12:00 - 14:00");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower().Contains("filipino"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[2], "Monday: 15:00 - 16:30");
                builder.AddField(subjects[2], "Thursday: 15:00 - 16:30");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower().Contains("purposive"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[3], "Monday: 19:30 - 21:00");
                builder.AddField(subjects[3], "Thursday: 19:30 - 21:00");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower().Contains("politic"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[4], "Tuesday: 09:00 - 10:30");
                builder.AddField(subjects[4], "Friday: 09:00 - 10:30");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower().Contains("computing") | x.ToLower().Contains("intro"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[5], "Tuesday: 10:30 - 13:30");
                builder.AddField(subjects[5], "Friday: 11:30 - 13:30");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower().Contains("math"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[6], "Tuesday: 15:00 - 16:30");
                builder.AddField(subjects[6], "Friday: 15:00 - 16:30");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            else if (x.ToLower().Contains("civic") | x.ToLower().Contains("cwts") | x.ToLower().Contains("train"))
            {
                var builder = new DiscordEmbedBuilder
                {
                    Title = $"{x}",
                    Color = DiscordColor.Yellow,
                };
                builder.AddField(subjects[7], "Sunday: 08:00 - 17:00");
                var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
            }
            #endregion
            else if (x.ToLower() == "easter")
            {
                if (now.DayOfWeek.ToString() == "Saturday" & now.Hour > 22)
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "Tangina, Sunday nanaman bukas.",
                        Color = DiscordColor.Red,
                    };
                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
                else if (now.DayOfWeek.ToString() == "Sunday" & now.Hour > 21 & now.Hour < 23)
                {
                    var builder = new DiscordEmbedBuilder
                    {
                        Title = "gising ka pa? gago, may pasok bukas.",
                        Color = DiscordColor.Red,
                        ImageUrl = "https://media.discordapp.net/attachments/896667151824478208/901844279452913715/image0.png?width=567&height=559"
                    };
                    var embed = await ctx.Channel.SendMessageAsync(embed: builder).ConfigureAwait(false);
                }
            }
        }
    }
}