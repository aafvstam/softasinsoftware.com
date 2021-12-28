using softasinsoftware.Shared.Models;

namespace softasinsoftware.API.Services
{
    public interface IYouTubeVideosService
    {
        Task<YouTubeVideoList> GetYouTubePlayListVideosAsync(string playlistID, int numberOfShows = 24, bool disableCache = false /* ClaimsPrincipal user */);
    }
}