﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace softasinsoftware.Web.Components
{
    public partial class SocialButtonComponent
    {
        [Parameter]
        public string AccountId { get; set; } = String.Empty;

        [Parameter]
        public string Title { get; set; } = String.Empty;

        [Inject]
        public IJSRuntime JsRuntime { get; set; } = default!;

        public string Icon { get; set; } = String.Empty;
        internal string Url { get; set; } = String.Empty;

        public async Task NavigateToNewTab()
        {
            await JsRuntime.InvokeVoidAsync("open", Url, "_blank");
        }
    }
}
