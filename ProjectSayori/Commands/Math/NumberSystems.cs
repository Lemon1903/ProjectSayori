using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace ProjectSayori.Commands
{

    public class NumberSystem : BaseCommandModule
    {
        string Base16InvalidCharacters = "ghijklmnopqrstuvwxyz";
        string Base2InvalidCharacters = "23456789abcdefghijklmnopqrstuvwxyz";
        string Base8InvalidCharacters = "89abcdefghijklmnopqrstuvwxyz";

        [Command("convert")]
        [Description("Converts the given number to the base wanted by the user.")]
        public async Task Main(CommandContext ctx, [Description("The Base Number of the given")] int initialBase,
                                                     [Description("The Base Number to convert")] int toConvertBase,
                                                                         [Description("Given")]  string number)
        {
            if ((initialBase != 2 & initialBase != 8 & initialBase != 10 & initialBase != 16) |
                (toConvertBase != 2 & toConvertBase != 8 & toConvertBase != 10 & toConvertBase != 16))
            {
                await ctx.RespondAsync("Please choose 2, 8, 10, or 16 for the base.");
            }
            else if (initialBase == toConvertBase)
            {
                await ctx.RespondAsync($"`Bruh.`");
            }
            else
            {
                await ctx.RespondAsync(Convertion(initialBase, toConvertBase, number));
            }
        }
        public string Convertion(int initial, int convertTo, string number)
        {
            string results = "";
            bool isNumeric = true;
            int remainder = 0;

            int length = number.Length;
            int power = length - 1;
            int total = 0;
            double product = 0;

            int a = 0;
            int b = 0;

            string Base16Character = "";

            switch (initial)
            {
                case 2:
                    #region Character Check

                    for (int i = 0; i < Base2InvalidCharacters.Length; i++)
                    {
                        if (number.ToLower().Contains(Base2InvalidCharacters[i]))
                        {
                            results = "Wait, this number isn't Base 2!";
                            isNumeric = false;
                            break;
                        }
                    } 

                    if (isNumeric == false)
                    {
                        break;
                    }
                    #endregion

                    switch (convertTo)
                    {
                        case 8:
                            results = "Still in progress";
                            break;
                        case 16:
                            results = "Still in progress";
                            break;
                        case 10:
                            for (int i = 0; i < length; i++)
                            {
                                int parse = Int32.Parse(Convert.ToString(number[i]));
                                product = parse * Math.Pow(2, power - i); 
                                total = total + Convert.ToInt32(product);
                            }
                            results = Convert.ToString(total);
                            break;
                    }
                    break;
                case 8:
                    #region Character Check

                    for (int i = 0; i < Base8InvalidCharacters.Length; i++)
                    {
                        if (number.ToLower().Contains(Base8InvalidCharacters[i]))
                        {
                            results = "Wait, this number isn't Base 8!";
                            isNumeric = false;
                            break;
                        }
                    }
                    if (isNumeric == false)
                    {
                        break;
                    }
                    #endregion

                    switch (convertTo)
                    {
                        case 2:
                            results = "Still in progress";
                            break;
                        case 16:
                            results = "Still in progress";
                            break;
                        case 10:
                            for (int i = 0; i < length; i++)
                            {
                                int parse = Int32.Parse(Convert.ToString(number[i]));
                                product = parse * Math.Pow(8, power - i);
                                total = total + Convert.ToInt32(product);
                            }
                            results = Convert.ToString(total);
                            break;
                    }
                    break;
                case 10:
                    #region Character Check
                    for (int i = 0; i < number.Length; i++)
                    {
                        if (Char.IsNumber(number, i))
                        {
                            continue;
                        }
                        else
                        {
                            results = "Wait, this number isn't Base 10!";
                            isNumeric = false;
                            break;
                        }
                    }
                    if (isNumeric == false) 
                    {
                        break; 
                    }
                    #endregion

                    a = Convert.ToInt32(number);
                    switch (convertTo)
                    {
                        case 2:
                            while (a != 0)
                            {
                                b = a / 2;
                                remainder = a % 2;
                                results = Convert.ToString(remainder) + results;
                                a = b;
                            }
                            break;
                        case 8:
                            while (a != 0)
                            {
                                b = a / 8;
                                remainder = a % 8;
                                results = Convert.ToString(remainder) + results;
                                a = b;
                            }
                            break;
                        case 16:
                            while (a != 0)
                            {
                                b = a / 16;
                                remainder = a % 16;
                                switch (remainder)
                                {
                                    case 10:
                                        results = "A" + results;
                                        break;
                                    case 11:
                                        results = "B" + results;
                                        break;
                                    case 12:
                                        results = "C" + results;
                                        break;
                                    case 13:
                                        results = "D" + results;
                                        break;
                                    case 14:
                                        results = "E" + results;
                                        break;
                                    case 15:
                                        results = "F" + results;
                                        break;
                                    default:
                                        results = Convert.ToString(remainder) + results;
                                        break;
                                }
                                a = b;
                            }
                            break;
                    }
                    break;
                case 16:
                    #region Character Check
                    for (int i = 0; i < Base16InvalidCharacters.Length; i++)
                    {
                        if (number.ToLower().Contains(Base16InvalidCharacters[i]))
                        {
                            results = "Wait, this number isn't Base 16!";
                            isNumeric = false;
                            break;
                        }
                    }

                    if (isNumeric == false)
                    {
                        break;
                    }
                    #endregion

                    switch (convertTo)
                    {
                        case 8:
                            results = "Still in progress";
                            break;
                        case 2:
                            results = "Still in progress";
                            break;
                        case 10:
                            for (int i = 0; i < length; i++)
                            {
                                Base16Character = Convert.ToString(number[i]);
                                int parse;
                                switch (Base16Character.ToLower())
                                {
                                    case "a":
                                        parse = 10;
                                        break;
                                    case "b":
                                        parse = 11;
                                        break;
                                    case "c":
                                        parse = 12;
                                        break;
                                    case "d":
                                        parse = 13;
                                        break;
                                    case "e":
                                        parse = 14;
                                        break;
                                    case "f":
                                        parse = 15;
                                        break;
                                    default:
                                        parse = Int32.Parse(Convert.ToString(number[i]));
                                        break;
                                }
                                product = parse * Math.Pow(16, power - i);
                                total = total + Convert.ToInt32(product);
                            }
                            results = Convert.ToString(total);
                            break;
                    }
                    break;
            }
            return $"`{results}`";
        }
    }
}
