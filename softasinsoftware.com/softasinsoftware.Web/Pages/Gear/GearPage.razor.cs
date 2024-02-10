using Microsoft.AspNetCore.Components;

using softasinsoftware.API.Services;
using softasinsoftware.Shared.Models;

using System.Text.Json;

namespace softasinsoftware.Web.Pages.Gear
{
    public partial class GearPage
    {
        public List<GearItem>? GearList { get; private set; }
        public string? ImageBaseAddress = string.Empty;

        [Inject]
        public ApiService? ApiService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (ApiService == null) return;

            var client = ApiService.HttpClient;

            if (client == null) return;
            if (client.BaseAddress != null)
            {
                ImageBaseAddress = client.BaseAddress.ToString();
            }

            HttpResponseMessage response = await client.GetAsync("gearlist");

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                if (responseStream != null)
                {
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    this.GearList = await JsonSerializer.DeserializeAsync<List<GearItem>>(responseStream, options);
                }
            }
        }
    }
}