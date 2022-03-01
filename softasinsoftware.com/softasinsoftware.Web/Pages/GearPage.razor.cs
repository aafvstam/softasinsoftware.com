using Microsoft.AspNetCore.Components;
using softasinsoftware.Shared.Models;
using System.Text.Json;

namespace softasinsoftware.Web.Pages
{
    public partial class GearPage
    {
        public List<GearItem> GearList { get; private set; } = new();

        //protected override void OnInitialized()
        //{
        //    GearList.AddRange(new List<GearItem>
        //    {
        //         new GearItem { Name= "Rode Procaster Broadcast Dynamic Vocal Microphone", URL="https://www.amazon.com/gp/product/B001IPUJJI/ref=as_li_tl?ie=UTF8&tag=softasinsoftw-20&camp=1789&creative=9325&linkCode=as2&creativeASIN=B001IPUJJI&linkId=738b836b5234dcc7c7f438fafe878d57", ShortURL="https://amzn.to/3qHU4hz"  },
        //         new GearItem { Name= "Elgato Lights", Description="", URL = "https://www.amazon.com/gp/product/B07L755X9G/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B07L755X9G&linkCode=as2&tag=softasinsoftw-20&linkId=73f92f8d3ad91250ce55dcb0a1be4234", ShortURL="https://amzn.to/32OHVPP"},
        //         new GearItem { Name= "Sony A6400", Description="", URL = "https://www.amazon.com/gp/product/B07MTWVN3M/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B07MTWVN3M&linkCode=as2&tag=softasinsoftw-20&linkId=12b8c7fc88b617927224dca5f907e8e5", ShortURL="https://amzn.to/32DupPb"},
        //         new GearItem { Name= "Sigma Lens", Description="", URL = "https://www.amazon.com/gp/product/B01C3SCKI6/ref=as_li_tl?ie=UTF8&camp=1789&creative=9325&creativeASIN=B01C3SCKI6&linkCode=as2&tag=softasinsoftw-20&linkId=f25233cb191e7a0d91b8a608790779df", ShortURL="https://amzn.to/3qL1noH"}
        //    });
        //}

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

                    List<GearItem> gearlist = await JsonSerializer.DeserializeAsync<List<GearItem>>(responseStream, options);

                    if (gearlist != null)
                    {
                        GearList = gearlist;
                    }
                }
            }
        }
    }
}