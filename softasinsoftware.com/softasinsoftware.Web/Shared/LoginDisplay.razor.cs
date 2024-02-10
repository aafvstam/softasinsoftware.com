using Microsoft.AspNetCore.Components;

using softasinsoftware.API.Services;

using System.Text.Json;

namespace softasinsoftware.Web.Shared
{
    public partial class LoginDisplay
    {
        [Inject]
        public ApiService? ApiService { get; set; }

        public int UserCount { get; private set; } = -1;

        protected override async Task OnInitializedAsync()
        {
            await GetUserCount();
        }

        private async Task GetUserCount()
        {
            if (ApiService == null) return;

            var client = ApiService.HttpClient;

            HttpResponseMessage response = await client.GetAsync("usercount");

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                if (responseStream != null)
                {
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    int usercount = await JsonSerializer.DeserializeAsync<int>(responseStream, options);

                    UserCount = usercount;
                }
            }
        }
    }
}
