using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSayoriRevised.Commands.DanbooruApi
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string File_Url { get; set; }
        public string Tag_String_Copyright { get; set; }
        public string Tag_String_Artist { get; set; }
    }
}