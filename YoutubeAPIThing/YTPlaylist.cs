using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace YoutubeAPIThing
{
    public class YTPlaylist
    {
        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public int TrackCount { get; internal set; }

        
        public string Url { get => "https://www.youtube.com/playlist?list=" + Id; }

        internal YTPlaylist(string id, string name, int videoCount) => (Id, Name, TrackCount) = (id, name, videoCount);




        private static async Task<string> GetHtml(string query)
        {
            string url = $"https://www.youtube.com/results?search_query={HttpUtility.UrlEncode(query)}&sp=EgIQAw%253D%253D";
            var request = WebRequest.CreateHttp(url);

            request.Method = "GET";
            using var resp = await request.GetResponseAsync();
            using var stream = resp.GetResponseStream();
            using var reader = new StreamReader(stream);

            string html = await reader.ReadToEndAsync();

            return html;
        }
        private static IEnumerable<YTPlaylist> GetIdsFromHtml(string html)
        {
            string start = "{\"itemSectionRenderer\":{\"contents\":";

            html = html[(html.IndexOf(start) + start.Length)..];

            int count = 1;
            int end = 1;
            for (int i = 1; count != 0; end = ++i)
            {
                var ch = html[i];
                if (ch == '[') count++;
                else if (ch == ']') count--;
            }

            html = html[..end];

            var list = JsonConvert.DeserializeObject<List<Json.JsonHelper>>(html);

            var playlistList = new List<YTPlaylist>();

            foreach (var l in list)
            {
                if (l.isPlaylist) playlistList.Add(l.playlistRenderer);
            }
            


            return playlistList;

        }
        public static async Task<IEnumerable<YTPlaylist>> SearchPlaylist(string query)
        {
            var html = await GetHtml(query);


            return GetIdsFromHtml(html);
        }

    }

}
