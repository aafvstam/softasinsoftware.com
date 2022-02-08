using System.Text.Json.Serialization;

namespace softasinsoftware.Shared.Models
{
    public class YouTubeVideo
    {
        public string Id { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty ;
        public string DisplayTitle { get; set; } = String.Empty ;
        public string Topic { get; set; } = String.Empty;
        
        public bool HasDisplayTitle { get; set; } = default ;
        public bool HasLinks { get; set; } = default ;

        public string LinksUrl { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public DateTime? ScheduledStartTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }

        // https://www.youtube.com/watch?v=a2uw7H_lWNA&list=PL06M1KyGksDAAjzsKLC0VNDCMmT7NcbTk&index=1&ab_channel=SoftAsInSoftware
        public string Url { get; set; } = String.Empty;

        // https://i.ytimg.com/vi/F_c35DzPJI8/hqdefault.jpg
        public string ThumbnailUrl { get; set; } =String.Empty;

        public string Category { get; set; } = String.Empty;

        public DateTimeOffset ShowDate { get; set; }

        public string Provider { get; set; } = String.Empty;
        public string ProviderId { get; set; } = String.Empty;

        [JsonIgnore]
        public bool IsNew => !IsInFuture && 
                             !IsOnAir &&
                             !(ScheduledStartTime == null) &&
                             (DateTime.UtcNow - ScheduledStartTime.Value).TotalDays <= 14;
        
        [JsonIgnore]
        public bool IsInFuture
        {
            get
            {
                return (this.LiveBroadcastContent.Equals("upcoming", StringComparison.OrdinalIgnoreCase) == true);
            }
        }


        [JsonIgnore]
        public bool IsOnAir
        {
            get 
            {
                return (this.LiveBroadcastContent.Equals("live", StringComparison.OrdinalIgnoreCase) == true);
            }
        }

        public static bool CheckHasStarted(DateTime utcNow, DateTime scheduled)
        {
            return utcNow > scheduled.AddMinutes(-5) && utcNow < scheduled.AddHours(2);
        }

        public string LiveBroadcastContent { get; set; } = String.Empty;
    }
}
