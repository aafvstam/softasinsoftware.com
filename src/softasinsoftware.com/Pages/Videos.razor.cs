using softasinsoftware.com.Models;

namespace softasinsoftware.com.Pages
{
    public partial class Videos
    {
        public IEnumerable<Video> VideoList { get; private set; } = new List<Video>();

        //TODO Take out hardcode URL
        public string MoreShowsUrl => "https://www.youtube.com/playlist?list=PL06M1KyGksDAAjzsKLC0VNDCMmT7NcbTk";
    }
}
