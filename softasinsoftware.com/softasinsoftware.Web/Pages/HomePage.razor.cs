using Microsoft.AspNetCore.Components;

using softasinsoftware.Shared.Models;
using softasinsoftware.Web.Models;

using System.Text.Json;


namespace softasinsoftware.Web.Pages
{
    public partial class HomePage

    {
        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        public YouTubeVideoList Videos { get; private set; } = new YouTubeVideoList();

        public string MoreShowsUrl { get; private set; } = String.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            HttpResponseMessage response = await client.GetAsync("youtubeplaylistvideos");

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                if (responseStream != null)
                {
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    YouTubeVideoList? videos = await JsonSerializer.DeserializeAsync<YouTubeVideoList>(responseStream, options);

                    if (videos != null)
                    {
                        Videos = videos;
                    }
                }
            }
        }
    }
}
