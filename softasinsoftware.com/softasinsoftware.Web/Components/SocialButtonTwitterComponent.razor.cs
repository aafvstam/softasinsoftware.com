namespace softasinsoftware.Web.Components
{
    public class SocialButtonTwitterComponentBase : SocialButtonComponentBase
    {
        public SocialButtonTwitterComponentBase()
        {
            Icon = "fab fa-twitter";
        }

        protected override void OnInitialized()
        {
            Url = "https://twitter.com/" + AccountId;
        }
    }
}
