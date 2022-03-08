using Google.Apis.Services;
using Google.Apis.YouTube.v3;

using Microsoft.Extensions.Caching.Memory;

using softasinsoftware.Shared.Models;

using System.Globalization;
using System.Text.Encodings.Web;

namespace softasinsoftware.API.Services
{
    public class YouTubeVideosService : IYouTubeVideosService
    {
        public IConfiguration Configuration { get; private set; }
        public IMemoryCache MemoryCache { get; private set; }

        private int NumberOfShows{ get; set; }
        private string PlayList { get; set; } = string.Empty;
        private string ApiKey { get; set; } = string.Empty;

        public YouTubeVideosService(IConfiguration configuration, IMemoryCache cache)
        {
            this.Configuration = configuration;
            this.MemoryCache = cache;   
        }

        public async Task<YouTubeVideoList> GetYouTubePlayListVideosAsync(string playlistID, int numberOfShows, bool disablecache)
        {
            this.PlayList = this.Configuration[playlistID];
            this.NumberOfShows = numberOfShows;
            this.ApiKey = this.Configuration["YouTube:ApiKey"];

            if (string.IsNullOrEmpty(this.ApiKey) ||
                string.IsNullOrEmpty(this.PlayList))
            {
                // return mockdata ...
            }

            if (true /* authenticated */ && disablecache)
            {
                // return uncached data direct from source (YouTube)
                return await GetVideosList(this.PlayList);
            }

            string cacheKey = this.PlayList;

            var result = MemoryCache.Get<YouTubeVideoList>(cacheKey);

            if (result == null)
            {
                result = await GetVideosList(this.PlayList);

                MemoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });
            }

            // lookup cache and collect
            return result;
        }

        private async Task<YouTubeVideoList> GetVideosList(string playlist)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = this.Configuration["YouTube:ApiKey"],
                ApplicationName = "SoftAsInSoftware"                // _appSettings.YouTubeApplicationName,
            });

            if (string.IsNullOrEmpty(youtubeService.ApiKey))
			{
                //TODO: return mock jsonfile 
			}

            var listRequest = youtubeService.PlaylistItems.List("snippet");
            listRequest.PlaylistId = playlist;
            listRequest.MaxResults = this.NumberOfShows;

            var playlistItems = await listRequest.ExecuteAsync();

            var result = new YouTubeVideoList
            {
                YouTubeVideos = playlistItems.Items.Select(item => new YouTubeVideo
                {
                    Provider = "YouTube",
                    ProviderId = item.Snippet.ResourceId.VideoId,
                    Title = item.Snippet.Title, // GetUsefulBitsFromTitle(item.Snippet.Title),
                    Description = item.Snippet.Description,
                    ThumbnailUrl = item.Snippet.Thumbnails.Medium != null ? item.Snippet.Thumbnails.Medium.Url : "https://wwww.softasinsoftware.com/dummy.jpg",
                    Url = GetVideoUrl(item.Snippet.ResourceId.VideoId, item.Snippet.PlaylistId, item.Snippet.Position ?? 0)
                }).ToList()
            };

            foreach (var show in result.YouTubeVideos)
            {
                var videoRequest = youtubeService.Videos.List("snippet"); // client
                videoRequest.Id = show.ProviderId; // videoID
                videoRequest.MaxResults = 1;

                var video = await videoRequest.ExecuteAsync();

                if (video.Items.Count > 0)
                {
                    var snippet = video.Items[0].Snippet;
                    show.ShowDate = DateTimeOffset.Parse(snippet.PublishedAtRaw, null, DateTimeStyles.RoundtripKind);
                    show.LiveBroadcastContent = snippet.LiveBroadcastContent;
                }
            }

            if (!string.IsNullOrEmpty(playlistItems.NextPageToken))
            {
                result.MoreVideosUrl = GetPlaylistUrl(playlist);
            }

            return result;
        }

        private static string GetVideoUrl(string id, string playlistId, long itemIndex)
        {
            var encodedId = UrlEncoder.Default.Encode(id);
            var encodedPlaylistId = UrlEncoder.Default.Encode(playlistId);
            var encodedItemIndex = UrlEncoder.Default.Encode(itemIndex.ToString());

            return $"https://www.youtube.com/watch?v={encodedId}&list={encodedPlaylistId}&index={encodedItemIndex}";
        }

        private static string GetPlaylistUrl(string playlistId)
        {
            var encodedPlaylistId = UrlEncoder.Default.Encode(playlistId);

            return $"https://www.youtube.com/playlist?list={encodedPlaylistId}";
        }


        private static class DesignData
        {
            public static readonly List<YouTubeVideo> Videos = new()
            {
                new YouTubeVideo
                {
                    ShowDate = new DateTime(2015, 7, 21, 9, 30, 0),
                    Title = "Soft as in Software - July 21st 2015",
                    Provider = "YouTube",
                    ProviderId = "7O81CAjmOXk",
                    ThumbnailUrl = "http://img.youtube.com/vi/7O81CAjmOXk/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=7O81CAjmOXk&index=1&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },
                new YouTubeVideo
                {
                    ShowDate = new DateTime(2015, 7, 14, 15, 30, 0),
                    Title = "Soft as in Software - July 14th 2015",
                    Provider = "YouTube",
                    ProviderId = "bFXseBPGAyQ",
                    ThumbnailUrl = "http://img.youtube.com/vi/bFXseBPGAyQ/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=bFXseBPGAyQ&index=2&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },

                new YouTubeVideo
                {
                    ShowDate = new DateTime(2015, 7, 7, 15, 30, 0),
                    Title = "Soft as in Software - July 7th 2015",
                    Provider = "YouTube",
                    ProviderId = "APagQ1CIVGA",
                    ThumbnailUrl = "http://img.youtube.com/vi/APagQ1CIVGA/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=APagQ1CIVGA&index=3&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },
                new YouTubeVideo
                {
                    ShowDate = DateTime.Now.AddDays(-28),
                    Title = "Soft as in Software - July 21st 2015",
                    Provider = "YouTube",
                    ProviderId = "7O81CAjmOXk",
                    ThumbnailUrl = "http://img.youtube.com/vi/7O81CAjmOXk/mqdefault.jpg",
                    Url = "https://www.youtube.com/watch?v=7O81CAjmOXk&index=1&list=PL0M0zPgJ3HSftTAAHttA3JQU4vOjXFquF"
                },
            };
        }
    }
}
