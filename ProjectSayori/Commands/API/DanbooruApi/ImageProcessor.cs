using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSayori.Commands.DanbooruApi
{
    public class ImageProcessor
    {
        public static async Task<ImageModel> LoadImage()
        {
            string url = "https://danbooru.donmai.us/posts/random.json?tags=rating%3Asafe";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                ImageModel image = await response.Content.ReadAsAsync<ImageModel>();
                return image;
            }
        }

        public static async Task<ImageModel> LoadImageGenshinNSFW(string character)
        {
            string url = "";
            if (character == null)
            {
                url = "https://danbooru.donmai.us/posts/random.json?tags=genshin_impact+rating%3Aexplicit";
            }
            else
            {
                url = "https://danbooru.donmai.us/posts/random.json?tags=genshin_impact+rating%3Aexplicit+" + character + "_(genshin_impact)";
            }
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ImageModel image = await response.Content.ReadAsAsync<ImageModel>();

                    return image;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}