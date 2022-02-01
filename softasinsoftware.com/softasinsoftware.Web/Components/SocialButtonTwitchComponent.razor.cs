namespace softasinsoftware.Web.Components
{
    public class SocialButtonTwitchComponentBase : SocialButtonComponentBase
    {
        public SocialButtonTwitchComponentBase()
        {
            Icon = "fab fa-twitch";
        }

        protected override void OnInitialized()
        {
            Url = "https://twitch.tv/" + AccountId;
        }
    }
}
