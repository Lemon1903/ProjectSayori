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
    public class Schedules : BaseCommandModule
    {
        private readonly string[] daysOfWeek = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        private static readonly string[] subjects = new string[]
        {
            /*0*/ "Discrete Structures",
            /*1*/ "World Literature",
            /*2*/ "Data Structures and Algorithms",
            /*3*/ "Object Oriented Programming",
            /*4*/ "Ethics",
            /*5*/ "Individual/Dual/Combative Sports",
            /*6*/ "Modeling and Simulation",
            /*7*/ "Logic Design and Digital Computer Circuits",
            /*8*/ "CS Free Elective"
        };
        private readonly Dictionary<string, string[,]> schedules = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Monday", new string[,] {
                { subjects[0], "07:30 - 09:00" },
                { subjects[1], "12:00 - 13:30" },
                { subjects[2], "14:30 - 16:30" },
                { subjects[3], "16:30 - 19:00" },
                { subjects[4], "19:30 - 21:00" },
            }},
            
            { "Tuesday", new string[,] {
                { subjects[5], "15:00 - 17:00" },
                { subjects[6], "18:00 - 16:30" },
            }},

            { "Thursday", new string[,] {
                { subjects[1], "12:00 - 13:30" },
                { subjects[2], "13:30 - 16:30" },
                { subjects[3], "17:00 - 19:30" },
                { subjects[4], "19:30 - 21:00" },
            }},

            { "Friday", new string[,] {
                { subjects[7], "18:00 - 21:00" },
            }},

            { "Saturday", new string[,] {
                { subjects[8], "12:00 - 15:00" },
            }},
        };
        private readonly Dictionary<string, string[]> permalinks = new()
        {
            { "Monday", new string[] {
                "To be announced...",
                "To be announced...",
                "To be announced...",
                "To be announced...",
                "To be announced...",
            }},

            { "Tuesday", new string[] {
                "To be announced...",
                "To be announced...",
            }},

            { "Thursday", new string[] {
                "To be announced...",
                "To be announced...",
                "To be announced...",
                "To be announced...",
            }},

            { "Friday", new string[] {
                "To be announced...",
            }},

            { "Saturday", new string[] {
                "To be announced...",
            }},
        };


        [Command("schedule"), Description("Tells what class you must be in right now.")]
        public async Task Schedule(CommandContext ctx)
        {
            DateTime now = DateTime.Now;
            string dayToday = now.DayOfWeek.ToString();
            var builder = BuildEmbedMessage(DiscordColor.Red, $"Today is {dayToday}", dayToday);
            await ctx.Channel.SendMessageAsync(builder);
        }

        [Command("schedule")]
        public async Task Schedule(CommandContext ctx, [RemainingText] [Description("subject, <day>, <class>")] string input)
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
                return $"There is no class yet. Wait 'till {firstTimeOfSched.Hours} for {schedToday[0, 0]}.";

            // return the link within the sched time if there is
            for (int i = 0; i < subjectTimes.Count; i++)
            {   
                // if there's no current subject but later... 
                if (timeToday < subjectTimes[i][0] && timeToday < subjectTimes[i][1])
                    return $"No current subject.\nWait till {subjectTimes[i][0]} for {schedToday[i, 0]}.";

                // if the subject is within the schedule
                if (timeToday >= subjectTimes[i][0] && timeToday <= subjectTimes[i][1])
                    return $"{schedToday[i, 0]}\nPermalink: {permalinks[dayToday][i]}";
            }

            // Return if there is no current subject on that time.
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
                builder.Description = "There will be no classes for that day!";
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