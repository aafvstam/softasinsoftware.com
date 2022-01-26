namespace softasinsoftware.Web.Components
{
    public class SocialButtonTwitchBase : SocialButtonBase
    {
        public SocialButtonTwitchBase()
        {
            Icon = "fab fa-twitch";
        }

        protected override void OnInitialized()
        {
            Url = "https://twitch.tv/" + AccountId;
        }
    }
}
