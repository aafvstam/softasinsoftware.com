using softasinsoftware.com.Models;

namespace softasinsoftware.com.Pages
{
    public partial class Index
    {
        public IEnumerable<Video> VideoList { get; set; }

        public Index()
        {
            //TODO: Create MockData and Service to collect videos from API
            VideoList = Enumerable.Empty<Video>();

            Video video = new Video();
            {
                video.Title = "Some Title";
                video.Description = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.";
                video.ShowDate = DateTime.Now;
                video.Url = "https://youtu.be/0H8yntZmq-s";
                video.ThumbnailUrl = "https://i.ytimg.com/vi/0H8yntZmq-s/hqdefault_live.jpg?sqp=-oaymwEcCPYBEIoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLDH7oj2IzdB4Nz3YbZcaiaX_Stsog";
            }

            for (int i = 0; i < 15; i++)
            {
                VideoList = VideoList.Append(video);
            }
        }   

        //TODO Take out hardcode URL
        public string MoreShowsUrl => "https://www.youtube.com/playlist?list=PL06M1KyGksDAAjzsKLC0VNDCMmT7NcbTk";
    }
}
