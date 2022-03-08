namespace softasinsoftware.Web.Components
{
    public partial class SocialButtonTwitterComponent : SocialButtonComponent
    {
        public SocialButtonTwitterComponent()
        {
            Icon = "fab fa-twitter";
        }

        protected override void OnInitialized()
        {
            Url = "https://twitter.com/" + AccountId;
        }
    }
}
