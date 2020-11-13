using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeAPIThing.Json
{
#pragma warning disable IDE1006
    internal class JsonHelper
    {
        public DidYouMeanRenderer didYouMeanRenderer { get; set; }
        public PlaylistRenderer playlistRenderer { get; set; }


        public bool isPlaylist { get => playlistRenderer is not null && didYouMeanRenderer is null; }
    }

    #region DidYouMean
    internal class DidYouMeanRenderer
    {
        public DidYouMean didYouMean { get; set; }
    }
    
    internal class DidYouMean
    {
        public List<TextJSON> runs { get; set; }
    }
    
    internal class TextJSON
    {
        public string text { get; set; }
    }
    #endregion



    internal class PlaylistRenderer
    {
        public string playlistId { get; set; }
        public Title title { get; set; }
        public string videoCount { get; set; }

        public static implicit operator YTPlaylist(PlaylistRenderer p)
           => new YTPlaylist(p.playlistId, p.title.simpleText, Convert.ToInt32(p.videoCount));
    }
    internal class Title
    {
        public string simpleText { get; set; }
    }
#pragma warning restore IDE1006
}
