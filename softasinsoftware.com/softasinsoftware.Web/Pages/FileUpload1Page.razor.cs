namespace softasinsoftware.Web.Pages
{
    public partial class FileUpload1Page
    {
        // Image

        string filename = "500jdonz.hrw";

        // [Inject]
        // public IHttpClientFactory? ClientFactory { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            /*            HttpResponseMessage response = await client.GetAsync("gearlist");
        if (ClientFactory == null)
        {
            return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");


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
            */
        }
    }
}
