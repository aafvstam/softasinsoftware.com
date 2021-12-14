using softasinsoftware.Shared.Models;

namespace softasinsoftware.Web.Pages
{
    public partial class Videos
    {
        public IEnumerable<YouTubeVideo> VideoList { get; private set; } = new List<YouTubeVideo>();

        //TODO Take out hardcode URL
        public static string MoreShowsUrl => "https://www.youtube.com/playlist?list=PL06M1KyGksDAAjzsKLC0VNDCMmT7NcbTk";
    }
}
