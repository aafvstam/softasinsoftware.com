namespace softasinsoftware.Web.Components
{
    public partial class SocialButtonYouTubeComponent : SocialButtonComponent
    {
        public SocialButtonYouTubeComponent()
        {
            Icon = "fab fa-youtube";
        }

        protected override void OnInitialized()
        {
            Url = "https://www.youtube.com/c/" + AccountId;
        }
    }
}
