using System.Text.Json.Serialization;

namespace softasinsoftware.Shared.Models
{
    public class YouTubeVideo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string DisplayTitle { get; set; }
        public string Topic { get; set; }
        
        public bool HasDisplayTitle { get; set; }
        public bool HasLinks { get; set; }

        public string LinksUrl { get; set; }
        public string Description { get; set; }

        public DateTime? ScheduledStartTime { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }

        // https://www.youtube.com/watch?v=a2uw7H_lWNA&list=PL06M1KyGksDAAjzsKLC0VNDCMmT7NcbTk&index=1&ab_channel=SoftAsInSoftware
        public string Url { get; set; }

        // https://i.ytimg.com/vi/F_c35DzPJI8/hqdefault.jpg
        public string ThumbnailUrl { get; set; }

        public string Category { get; set; }

        public DateTimeOffset ShowDate { get; set; }

        public string Provider { get; set; }
        public string ProviderId { get; set; }

        [JsonIgnore]
        public bool IsNew => !IsInFuture && 
                             !IsOnAir &&
                             (DateTime.UtcNow - ScheduledStartTime.Value).TotalDays <= 14;
        
        [JsonIgnore]
        public bool IsInFuture => ScheduledStartTime.Value > DateTime.UtcNow;

        [JsonIgnore]
        public bool IsOnAir
        {
            get 
            {
                if (ActualStartTime.HasValue && ActualEndTime.HasValue)
                {
                    return false;
                }

                if (ActualStartTime.HasValue && !ActualEndTime.HasValue)
                {
                    return true;
                }

                var scheduled = ScheduledStartTime.Value;
                return CheckHasStarted(DateTime.UtcNow, scheduled);
            }
        }

        public static bool CheckHasStarted(DateTime utcNow, DateTime scheduled)
        {
            return utcNow > scheduled.AddMinutes(-5) && utcNow < scheduled.AddHours(2);
        }

        public string LiveBroadcastContent { get; set; }
    }
}
