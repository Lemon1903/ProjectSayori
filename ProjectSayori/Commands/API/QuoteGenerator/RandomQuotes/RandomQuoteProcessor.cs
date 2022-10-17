using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using ProjectSayori.Commands.DanbooruApi;


namespace ProjectSayori.Commands.QuoteGenerator
{
        public class RandomQuoteModel
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
    }


    public class RandomQuoteProcessor
    {
        public static async Task<RandomQuoteModel> LoadRandomQuote(string tag="")
        {
            string url = $"https://api.quotable.io/random?tags={tag}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    RandomQuoteModel randomQuote = await response.Content.ReadAsAsync<RandomQuoteModel>();
                    return randomQuote;
                }

                return null;
            }
        }
    }
}