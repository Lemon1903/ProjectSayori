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
        private readonly string[] daysOfWeek = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        private static readonly string[] subjects = new string[]
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
        private readonly Dictionary<string, string[,]> schedules = new(StringComparer.OrdinalIgnoreCase)
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
                { subjects[5], "11:30 - 13:30" },
                { "(No Subject)", "13:30 - 15:00" },
                { subjects[6], "15:00 - 16:30" },
            }},

            { "Sunday", new string[,] {
                { subjects[7], "08:00 - 17:00" },
            }},
        };
        private readonly Dictionary<string, string[]> permalinks = new()
        {
            { "Monday", new string[] {
                "https://us02web.zoom.us/j/3205431145?pwd=UWlBb0JiVjJPZmsyem5wVlMrTS82QT09",
                "To be announced...",
                "Ignore this",
                "https://meet.google.com/vdv-nyjy-wng",
                "Ignore this",
                "To be announced...",
            }},

            { "Tuesday", new string[] {
                "https://meet.google.com/twn-czpk-ezw",
                "To be announced...",
                "Ignore this",
                "https://us02web.zoom.us/j/81384059714?pwd=WEt1NmZTcUIzWUkrajZvQ1N6OHRHZz09",
            }},

            { "Thursday", new string[] {
                "https://us02web.zoom.us/j/3205431145?pwd=UWlBb0JiVjJPZmsyem5wVlMrTS82QT09",
                "Ignore this",
                "https://meet.google.com/vdv-nyjy-wng",
                "Ignore this",
                "To be announced...",
            }},

            { "Friday", new string[] {
                "https://meet.google.com/twn-czpk-ezw",
                "Ignore this",
                "Asynchronous",
                "Ignore this",
                "To be announced...",
            }},

            { "Sunday", new string[] {
                "To be announced...",
            }},
        };


        [Command("schedule-copy"), Description("Tells what class you must be in right now.")]
        public async Task Schedule(CommandContext ctx)
        {
            DateTime now = DateTime.Now;
            string dayToday = now.DayOfWeek.ToString();
            var builder = BuildEmbedMessage(DiscordColor.Red, $"Today is {dayToday}", dayToday);
            await ctx.Channel.SendMessageAsync(builder);
        }

        [Command("schedule-copy")]
        public async Task Schedule(CommandContext ctx, [RemainingText]string input)
        {
            DateTime now = DateTime.Now;
            string dayToday = now.DayOfWeek.ToString();
            DiscordEmbedBuilder builder;
            
            if (input.Equals("subject", StringComparison.OrdinalIgnoreCase))
            {
                var response = GetResponseFromSubject(now, dayToday);
                builder = BuildEmbedMessage(response);
            }
            else if (input.Equals("easter", StringComparison.OrdinalIgnoreCase))
            {
                builder = BuildEmbedMessage(dayToday, now.Hour);
            }
            else if (daysOfWeek.Contains(input.ToLower()))
            {
                builder = BuildEmbedMessage(DiscordColor.Orange, $"{input.ToUpper()} schedule", input);
            }
            else
            {
                var subjectScheds = SearchByKeyword(input);
                builder = BuildEmbedMessage(subjectScheds.Item1, subjectScheds.Item2);
            }

            await ctx.Channel.SendMessageAsync(builder);
        }

        private string GetResponseFromSubject(DateTime now, string dayToday)
        {
            // check if no classes 
            if (!schedules.ContainsKey(dayToday))
                return "There is no class today!";

            var timeToday = now.TimeOfDay;
            var schedToday = schedules[dayToday];
            var subjectTimes = new List<TimeSpan[]>();

            // get all times with subject
            for (int i = 0; i < schedToday.GetLength(0); i++)
            {
                var timeEnumerable = from string subTime in schedToday[i, 1].Split("-") select TimeSpan.Parse(subTime);
                subjectTimes.Add(timeEnumerable.ToArray());
            }

            // check if current time is before the first subject time
            var firstTimeOfSched = subjectTimes[0][0];
            if (timeToday < firstTimeOfSched)
                return $"There is no class yet. Wait 'till {firstTimeOfSched.Hours}.";

            // return the link within the sched time if there is
            for (int i = 0; i < subjectTimes.Count; i++)
            {
                if (schedToday[i, 0] != "(No Subject)")
                    if (timeToday >= subjectTimes[i][0] && timeToday <= subjectTimes[i][1])
                        return $"{schedToday[i, 0]}\nPermalink: {permalinks[dayToday][i]}";
            }

            return "No current subject.";
        }

        private Tuple<string, List<string>> SearchByKeyword(string input)
        {
            var subjectScheds = new List<string>();
            var subject = String.Empty;

            // loop through whole sched and check if a subject matched the keyword
            foreach (var (day, sched) in schedules)
            {
                // loop through all the sched in that day
                for (int i = 0; i < sched.GetLength(0); i++)
                {
                    // store the subject and the time of that subject
                    if (sched[i, 0].IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        subject = sched[i, 0];
                        subjectScheds.Add($"{day}: {sched[i, 1]}");
                        break;
                    }
                }
            }

            return new Tuple<string, List<string>>(subject, subjectScheds);
        }

        #region Message Embed Builder Method Overloads
        private DiscordEmbedBuilder BuildEmbedMessage(string details)
        {
            var builder = new DiscordEmbedBuilder 
            { 
                Title = "Subject in current time.",
                Color = DiscordColor.Azure,
                Description = details,
            };

            return builder;
        }

        private DiscordEmbedBuilder BuildEmbedMessage(DiscordColor color, string title, string day)
        {
            var builder = new DiscordEmbedBuilder{ Title = title };

            if (schedules.ContainsKey(day))
            {
                for (int i = 0; i < schedules[day].GetLength(0); i++)
                {
                    builder.Color = color;
                    builder.AddField(schedules[day][i, 0], schedules[day][i, 1]);
                }
            }
            else
            {
                builder.Color = DiscordColor.Green;
                builder.Description = "There will be no classes for today!";
            }

            return builder;
        }

        private DiscordEmbedBuilder BuildEmbedMessage(string dayToday, int hour)
        {
            var builder = new DiscordEmbedBuilder{ Color = DiscordColor.Red };

            if (dayToday == "Saturday" && hour > 22)
            {
                builder.Title = "Aaaaaaa, Sunday nanaman bukas.";
            }
            else if (dayToday == "Sunday" && hour > 21 && hour < 23)
            {
                builder.Title = "gising ka pa? gagi, may pasok bukas.";
                builder.ImageUrl = "https://media.discordapp.net/attachments/896667151824478208/901844279452913715/image0.png?width=567&height=559";
            }

            return builder;
        }

        private DiscordEmbedBuilder BuildEmbedMessage(string subjectName, List<string>subjectSched)
        {
            var builder = new DiscordEmbedBuilder
            {
                Title = subjectName,
                Color = DiscordColor.Yellow,
                Description = string.Join("\n", subjectSched),
            };

            return builder;
        }
        #endregion
    }
}