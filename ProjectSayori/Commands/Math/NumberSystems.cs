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
                                                                          [Description("Given")] string number)
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
            // Base 10 Algorithm Variables
            string results = "";
            bool isNumeric = true;
            int remainder = 0;

            // "raise-to" Algorithm Variables
            int length = number.Length;
            int power = length - 1;
            int total = 0;
            double product = 0;

            // Parsing Variable
            int parse = 0;

            // Base 2 <-> 8/16 Variable
            string group = "";
            string divider = "";
            int loop = 0;
            string trimmed = "";
            bool one = false;

            // Base 16 ABCDEF Character Checker
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
                            switch (number.Length % 3)
                            {
                                case 1:
                                    number = "00" + number;
                                    break;
                                case 2:
                                    number = "0" + number;
                                    break;
                            }
                            loop = 0;
                            do
                            {
                                divider = $"{number[loop]}{number[loop + 1]}{number[loop + 2]}";
                                switch (divider)
                                {
                                    case "000":
                                        group = "0";
                                        break;
                                    case "001":
                                        group = "1";
                                        break;
                                    case "010":
                                        group = "2";
                                        break;
                                    case "011":
                                        group = "3";
                                        break;
                                    case "100":
                                        group = "4";
                                        break;
                                    case "101":
                                        group = "5";
                                        break;
                                    case "110":
                                        group = "6";
                                        break;
                                    case "111":
                                        group = "7";
                                        break;
                                }
                                results += group;
                                loop = loop + 3;
                            } while (loop < number.Length);
                            break;
                        case 16:
                            switch (number.Length % 4)
                            {
                                case 1:
                                    number = "000" + number;
                                    break;
                                case 2:
                                    number = "00" + number;
                                    break;
                                case 3:
                                    number = "0" + number;
                                    break;
                            }
                            loop = 0;
                            do
                            {
                                divider = $"{number[loop]}{number[loop + 1]}{number[loop + 2]}{number[loop + 3]}";
                                switch (divider)
                                {
                                    case "0000":
                                        group = "0";
                                        break;
                                    case "0001":
                                        group = "1";
                                        break;
                                    case "0010":
                                        group = "2";
                                        break;
                                    case "0011":
                                        group = "3";
                                        break;
                                    case "0100":
                                        group = "4";
                                        break;
                                    case "0101":
                                        group = "5";
                                        break;
                                    case "0110":
                                        group = "6";
                                        break;
                                    case "0111":
                                        group = "7";
                                        break;
                                    case "1000":
                                        group = "8";
                                        break;
                                    case "1001":
                                        group = "9";
                                        break;
                                    case "1010":
                                        group = "A";
                                        break;
                                    case "1011":
                                        group = "B";
                                        break;
                                    case "1100":
                                        group = "C";
                                        break;
                                    case "1101":
                                        group = "D";
                                        break;
                                    case "1110":
                                        group = "E";
                                        break;
                                    case "1111":
                                        group = "F";
                                        break;

                                }
                                results += group;
                                loop = loop + 4;
                            } while (loop < number.Length);
                            break;
                        case 10:
                            for (int i = 0; i < length; i++)
                            {
                                parse = Int32.Parse(Convert.ToString(number[i]));
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
                            for (int i = 0; i < length; i++)
                            {
                                parse = Int32.Parse(Convert.ToString(number[i]));
                                switch (parse)
                                {
                                    case 0:
                                        group = "000";
                                        break;
                                    case 1:
                                        group = "001";
                                        break;
                                    case 2:
                                        group = "010";
                                        break;
                                    case 3:
                                        group = "011";
                                        break;
                                    case 4:
                                        group = "100";
                                        break;
                                    case 5:
                                        group = "101";
                                        break;
                                    case 6:
                                        group = "110";
                                        break;
                                    case 7:
                                        group = "111";
                                        break;
                                }

                                results = results + group;
                            }

                            #region Remove leading 0's from the group
                            trimmed = "";
                            one = false;
                            for (int check = 0; check < results.Length; check++)
                            {
                                if (results[check].ToString() == "0" & one == false)
                                {
                                    continue;
                                }
                                else if (results[check].ToString() == "1")
                                {
                                    one = true;
                                    trimmed = trimmed + results[check];
                                }
                                else
                                {
                                    trimmed = trimmed + results[check];
                                }
                            }
                            #endregion

                            results = trimmed;
                            break;
                        case 10:
                            for (int i = 0; i < length; i++)
                            {
                                parse = Int32.Parse(Convert.ToString(number[i]));
                                product = parse * Math.Pow(8, power - i);
                                total = total + Convert.ToInt32(product);
                            }
                            results = Convert.ToString(total);
                            break;
                        case 16:
                            results = "Still in progress";
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

                    int a = Convert.ToInt32(number);
                    int b = 0;
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
                            for (int i = 0; i < length; i++)
                            {
                                Base16Character = Convert.ToString(number[i]);
                                switch (Base16Character.ToLower())
                                {
                                    case "0":
                                        group = "0000";
                                        break;
                                    case "1":
                                        group = "0001";
                                        break;
                                    case "2":
                                        group = "0010";
                                        break;
                                    case "3":
                                        group = "0011";
                                        break;
                                    case "4":
                                        group = "0100";
                                        break;
                                    case "5":
                                        group = "0101";
                                        break;
                                    case "6":
                                        group = "0110";
                                        break;
                                    case "7":
                                        group = "0111";
                                        break;
                                    case "8":
                                        group = "1000";
                                        break;
                                    case "9":
                                        group = "1001";
                                        break;
                                    case "a":
                                        group = "1010";
                                        break;
                                    case "b":
                                        group = "1011";
                                        break;
                                    case "c":
                                        group = "1100";
                                        break;
                                    case "d":
                                        group = "1101";
                                        break;
                                    case "e":
                                        group = "1110";
                                        break;
                                    case "f":
                                        group = "1111";
                                        break;
                                }

                                results += group;
                            }

                            #region Remove leading 0's from the group
                            trimmed = "";
                            one = false;
                            for (int check = 0; check < results.Length; check++)
                            {
                                if (results[check].ToString() == "0" & one == false)
                                {
                                    continue;
                                }
                                else if (results[check].ToString() == "1")
                                {
                                    one = true;
                                    trimmed = trimmed + results[check];
                                }
                                else
                                {
                                    trimmed = trimmed + results[check];
                                }
                            }
                            #endregion

                            results = trimmed;
                            break;
                        case 10:
                            for (int i = 0; i < length; i++)
                            {
                                Base16Character = Convert.ToString(number[i]);
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
