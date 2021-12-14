namespace softasinsoftware.Shared.Models
{
    public class YouTubeVideoList
    {
        public IEnumerable<YouTubeVideo> YouTubeVideos { get; set; } = Enumerable.Empty<YouTubeVideo>();

        public string MoreVideosUrl { get; set; } = string.Empty;
    }
}

