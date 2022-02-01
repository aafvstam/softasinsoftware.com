namespace softasinsoftware.Web.Components
{
    public class SocialButtonYouTubeComponentBase : SocialButtonComponentBase
    {
        public SocialButtonYouTubeComponentBase()
        {
            Icon = "fab fa-youtube";
        }

        protected override void OnInitialized()
        {
            Url = "https://www.youtube.com/c/" + AccountId;
        }
    }
}
