namespace softasinsoftware.Web.Models
{
    public class YouTubeAPIClientModel
    {
        public HttpClient Client { get; }

        public YouTubeAPIClientModel(HttpClient client)
        {
            Client = client;
        }
    }
}
