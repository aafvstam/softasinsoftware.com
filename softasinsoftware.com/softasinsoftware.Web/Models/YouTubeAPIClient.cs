namespace softasinsoftware.Web.Models
{
    public class YouTubeAPIClient
    {
        public HttpClient Client { get; }

        public YouTubeAPIClient(HttpClient client)
        {
            Client = client;
        }

    }
}
