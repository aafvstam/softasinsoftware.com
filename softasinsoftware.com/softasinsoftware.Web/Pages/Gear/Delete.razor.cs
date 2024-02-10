using Microsoft.AspNetCore.Components;

using softasinsoftware.API.Services;
using softasinsoftware.Shared.Models;

using System.Text.Json;

namespace softasinsoftware.Web.Pages.Gear
{
    partial class Delete
    {
        [Parameter]
        public int GearId { get; set; }

        public GearItem GearItem { get; set; } = new();

        [Inject]
        public ApiService? ApiService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (ApiService == null) return;

            var client = ApiService.HttpClient;

            HttpResponseMessage response = await client.GetAsync("gear/" + Convert.ToInt32(GearId));

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

        protected async Task RemoveGearItem(int GearId)
        {
            if (ApiService == null) return;

            var client = ApiService.HttpClient;

            HttpResponseMessage response = await client.DeleteAsync("gear/" + Convert.ToInt32(GearId));

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
