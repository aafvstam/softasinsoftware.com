﻿namespace softasinsoftware.Web.Components
{
    public partial class SocialButtonTwitchComponent
    {
        public SocialButtonTwitchComponent()
        {
            Icon = "fa-brands fa-twitch";
        }

        protected override void OnInitialized()
        {
            Url = "https://twitch.tv/" + AccountId;
        }
    }
}
