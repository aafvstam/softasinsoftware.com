namespace softasinsoftware.Web.Components
{
    public class SocialButtonYouTubeBase : SocialButtonBase
    {
        public SocialButtonYouTubeBase()
        {
            Icon = "fab fa-youtube";
        }

        protected override void OnInitialized()
        {
            Url = "https://www.youtube.com/c/" + AccountId;
        }
    }
}
