using Microsoft.AspNetCore.Components;

using softasinsoftware.Shared.Models;

using System.Text.Json;

namespace softasinsoftware.Web.Pages.Gear
{
    public partial class GearPage
    {
        public List<GearItem>? GearList { get; private set; }

        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

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