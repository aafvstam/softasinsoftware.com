using Microsoft.AspNetCore.Components;

using softasinsoftware.Shared.Models;

namespace softasinsoftware.Web.Pages
{
    public partial class YouTubeShow
    {
        [Parameter]
        public YouTubeVideo YouTubeVideo { get; set; } = default!;
    }
}
