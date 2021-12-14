using softasinsoftware.Shared.Models;

using System.Security.Claims;

namespace softasinsoftware.API.Services
{
    public interface IYouTubeVideosService
    {
        Task<YouTubeVideoList> GetYouTubePlayListVideosAsync(int numberOfShows = 24 /*ClaimsPrincipal user, bool disableCache, string playlist*/);
    }
}