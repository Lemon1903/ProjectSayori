using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using ProjectSayori.Commands.DanbooruApi;


namespace ProjectSayori.Commands.QuoteGenerator
{
    public class SearchQuoteModel
    {
        public int Count { get; set; }
        public int TotalPages { get; set; }
        public List<Dictionary<string, object>> Results { get; set; }
    }


    public class SearchQuoteProcessor
    {
        private static readonly Random random = new();
        private static SearchQuoteModel searchQuote;
        private static string url;

        public static async Task<RandomQuoteModel> SearchQuote(string query)
        {
            url = $"https://api.quotable.io/search/quotes?query={query}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                    searchQuote = await response.Content.ReadAsAsync<SearchQuoteModel>();
                else
                    return null;
            }

            int page = random.Next(1, searchQuote.TotalPages + 1);
            url = $"https://api.quotable.io/search/quotes?query={query}&page={page}";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    searchQuote = await response.Content.ReadAsAsync<SearchQuoteModel>();
                    if (searchQuote.Results.Count > 0)
                    {
                        int index = random.Next(searchQuote.Count + 1);
                        return new RandomQuoteModel
                        {
                            Content = (string)searchQuote.Results[index]["content"],
                            Author = (string)searchQuote.Results[index]["author"],
                            Tags = ((JArray)searchQuote.Results[index]["tags"]).ToObject<List<string>>(),
                        };
                    }
                }

                return null;
            }
        }
    }
}