using Microsoft.AspNetCore.Components;

using softasinsoftware.Shared.Models;

using System.Text.Json;

namespace softasinsoftware.Web.Pages
{
    public partial class InitDb

    {
        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        private bool ShowErrors;
        private IEnumerable<string> Errors;

        public int UserCount { get; private set; }
        public string UserID { get; private set; } = string.Empty;
        public string UserSecret { get; private set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await GetUserCount();
        }

        private async Task GetUserCount()
        {
            if (ClientFactory == null) return;

            var client = ClientFactory.CreateClient("softasinsoftware.API");

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

        private async void InitializeDb()
        {
            ShowErrors = false;

            if (ClientFactory == null) return;

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            HttpResponseMessage response = await client.PostAsync("register-admin", null);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                if (responseStream != null)
                {
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    RegisterResult result = await JsonSerializer.DeserializeAsync<RegisterResult>(responseStream, options);

                    if (result != null && result.Successful == false)
                    {
                        Errors = result.Errors;
                        ShowErrors = true;
                    }
                }

                await GetUserCount();
                StateHasChanged();
            }
        }
    }
}
