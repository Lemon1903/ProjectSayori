using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace ProjectSayori.Commands
{
    public class EuclideanAlgorithm : BaseCommandModule
    {
        [Command("gcd")]
        [Description("Gets the Greatest Common Divisor using the Euclidean Algorithm")]
        public async Task GCD(CommandContext ctx, [Description("Input first number")] int a,
                                                  [Description("Input second number")] int b)
        {
            // Swaps integer when b > a
            if (b > a)
            {
                (a, b) = (b, a);
            }

            // Zero, Negative, or Non-Zero Variables
            if (a == 0 | b == 0)
            {
                // Show the highest number as the GCD
                await ctx.RespondAsync("GCD: " + Math.Max(a, b));
            }
            else if (a < 0 | b < 0)
            {
                await ctx.RespondAsync("Values can't be below zero!");
            }
            else
            {
                // Declare variables as the quotient and remainder
                int c = a / b; // Quotient
                int r = a % b; // Remainder

                // Euclidean Algorithm Equation 
                ArrayList formula = new ArrayList();
                while (r != 0)
                {
                    formula.Add($"{a} = {b}*{c} + {r}");
                    a = b;
                    b = r;
                    c = a / b;
                    r = a % b;
                }

                // If Remainder is equal to zero, show the Euclidean Algorithm and GCD.
                formula.Add($"{a} = {b}*{c} + {r}");
                string display = "";
                foreach (object obj in formula)
                {
                    display = display + $"{obj}\n";
                }
                await ctx.RespondAsync($"```{display} \n \nGCD: {b} ```");
            }
        }
    }
}