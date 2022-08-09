using Microsoft.AspNetCore.Components;

using softasinsoftware.Shared.Models;

namespace softasinsoftware.Web.Pages.Gear
{
    public partial class GearCard
    {
        [Parameter]
        public GearItem GearItem { get; set; } = default!;

        [Parameter]
        public string? ImageBaseAddress { get; set; } = string.Empty;

        private string ImageSource { get; set; }

        protected override void OnInitialized()
        {
            ImageSource = ImageBaseAddress + $"imagedownload/" + GearItem.Image;
        }
    }
}
