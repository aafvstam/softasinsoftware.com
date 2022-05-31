using Microsoft.AspNetCore.Components;

using softasinsoftware.Shared.Models;

using System.Text.Json;

namespace softasinsoftware.Web.Pages.Gear
{
    partial class Delete
    {
        [Parameter]
        public int gearId { get; set; }

        public GearItem GearItem { get; set; } = new();

        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        [Inject]
        public NavigationManager? NavigationManager { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            HttpResponseMessage response = await client.GetAsync("gear/" + Convert.ToInt32(gearId));

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                if (responseStream != null)
                {
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    GearItem gearitem = await JsonSerializer.DeserializeAsync<GearItem>(responseStream, options);

                    if (gearitem != null)
                    {
                        GearItem = gearitem;
                    }
                }
            }
        }

        protected async Task RemoveGearItem(int userID)
        {
            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            HttpResponseMessage response = await client.DeleteAsync("gear/" + Convert.ToInt32(gearId));

            if (response.IsSuccessStatusCode)
            {
                this.NavigationManager.NavigateTo("/gear/gear");
            }
        }
        void Cancel()
        {
            this.NavigationManager.NavigateTo("/gear/gear");
        }
    }
}
