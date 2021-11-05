using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json.Linq;
using DSharpPlus.Entities;

namespace ProjectSayoriRevised.Commands
{
    public class Fibonacci : BaseCommandModule
    {
        [Command("fibonacci")]
        [Description("Tells the fibonacci number using the Binet's Formulla.")]
        public async Task Command(CommandContext ctx, [Description("x >= 0")] int x)
        {
            if (x < 0)
            {
                await ctx.RespondAsync("x cannot be below zero");
            }

            else
            {
                await ctx.RespondAsync(Binet(x));
            }
        }

        public string Binet(int num)
        {
            string results = "";

            double sqRt5 = Math.Sqrt(5);

            double fractionAdd = (1 + sqRt5) / 2;
            double fractionSubtract = (1 - sqRt5) / 2;

            double fractionAddPower = Math.Pow(fractionAdd, num);
            double fractionSubtractPower = (Math.Pow(fractionSubtract, num));

            double top = fractionAddPower - fractionSubtractPower;

            double answer = top / sqRt5;

            results = $"```n = {num}\n" +
                $"f{num} = ((1 + √5)^{num} - (1 - √5)^{num})/√5\n" +
                $"f{num} = ({Math.Round(fractionAdd, 2)}^{num} - {Math.Round(fractionSubtract, 2)}^{num})/√5\n" +
                $"f{num} = ({Math.Round(fractionAddPower, 2)} - {Math.Round(fractionSubtractPower, 2)})/√5\n" +
                $"f{num} = {Math.Round(top, 2)}/√5\n" +
                $"f{num} = {answer}\n" +
                $"f{num} ≈ {Math.Round(answer, 0)}```";

            return results;
        }
    }
}