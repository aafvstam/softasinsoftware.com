using Microsoft.AspNetCore.Components;

namespace softasinsoftware.Web.Pages
{
    public partial class InitDb

    {
        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            HttpResponseMessage response = await client.GetAsync("register-admin");

            if (response.IsSuccessStatusCode)
            {
                // Notify result
            }
        }
    }
}
