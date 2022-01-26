namespace softasinsoftware.Web.Components
{
    public class SocialButtonTwitterBase : SocialButtonBase
    {
        public SocialButtonTwitterBase()
        {
            Icon = "fab fa-twitter";
        }

        protected override void OnInitialized()
        {
            Url = "https://twitter.com/" + AccountId;
        }
    }
}
