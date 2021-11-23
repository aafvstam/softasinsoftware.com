namespace softasinsoftware.com.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ShowDate { get; set; }
        public string Url { get; set; }

        public string Provider { get; set; }
        public string ProviderId { get; set; }

        public bool HasTitle => !string.IsNullOrEmpty(Title);

        // Tags
        public bool IsNew => !IsInFuture && (DateTimeOffset.Now - ShowDate).TotalDays <= 7;
        public bool IsInFuture => ShowDate > DateTimeOffset.Now;

        // Do we need this?
        public string ThumbnailUrl { get; set; }
        public string LiveBroadcastContent { get; set; }
    }
}
