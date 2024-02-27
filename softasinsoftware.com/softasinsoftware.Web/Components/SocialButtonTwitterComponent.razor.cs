namespace softasinsoftware.Web.Components
{
    public partial class SocialButtonTwitterComponent : SocialButtonComponent
    {
        public SocialButtonTwitterComponent()
        {
            Icon = "fa-brands fa-x-twitter";
        }

        protected override void OnInitialized()
        {
            Url = "https://twitter.com/" + AccountId;
        }
    }
}
