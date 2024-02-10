namespace softasinsoftware.API.Services
{
    public class ApiService
    {
        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            HttpClient = httpClient;

            string? baseUrl;

            // Production-specific configuration
            // Read from appsettings.json
            if (configuration["ASPNETCORE_ENVIRONMENT"] == "Production")
            {
                baseUrl = configuration["ApiBaseUrl:Production"];
            }
            else
            {
                baseUrl = configuration["ApiBaseUrl:Development"];
            }

            if (string.IsNullOrEmpty(baseUrl) == false)
            {
                Uri uri = new Uri(baseUrl);
                if (HttpClient.BaseAddress != uri)
                {
                    HttpClient.BaseAddress = uri;
                }
            }
            else
            {
                Console.WriteLine("ApiBaseUrl not found in configuration, BaseAddress not set.");
            }
        }

        public HttpClient HttpClient { get; }
    }
}
