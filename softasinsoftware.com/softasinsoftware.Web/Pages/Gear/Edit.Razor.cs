using Microsoft.AspNetCore.Components;

using softasinsoftware.Shared.Models;

using System.Net.Http.Json;
using System.Text.Json;

namespace softasinsoftware.Web.Pages.Gear
{
    partial class Edit
    {
        [Parameter]
        public int gearId { get; set; }
        protected string Title = "Add";

        public GearItem GearItem { get; set; } = new();

        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        [Inject]
        public NavigationManager? NavigationManager { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            if (gearId != 0)
            {
                Title = "Edit";

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
        }

        protected async Task SaveGearItem()
        {
            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            HttpResponseMessage response = new();

            if (gearId != 0)
            {
                response = await client.PutAsJsonAsync("gear/" + Convert.ToInt32(gearId), GearItem);
            }
            else
            {
                response = await client.PostAsJsonAsync("gear/", GearItem);
            }

            if (response.IsSuccessStatusCode)
            {
                this.NavigationManager.NavigateTo("/gear/gear");
            }

            Cancel();
        }

        public void Cancel()
        {
            this.NavigationManager.NavigateTo("/gear/gear");
        }

    }
}
