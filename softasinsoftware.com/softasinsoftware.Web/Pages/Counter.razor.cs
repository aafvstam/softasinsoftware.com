using Microsoft.AspNetCore.Components;
//using Microsoft.JSInterop;

using System.Threading.Tasks;

namespace softasinsoftware.Web.Pages
{
    public partial class Counter
    {
        //[Inject]
        //public IJSRuntime JSRuntime { get; set; }

        //private IJSObjectReference _jsModule;

        public int CurrentCount { get; set; } = 0;

        private void IncrementCount()
        {
            CurrentCount += 2;
        }

        //protected override async Task OnInitializedAsync()
        //{
        //    _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/jsExamples.js");
        //}

        //private async Task ShowAlertWindow() =>
        //    await _jsModule.InvokeVoidAsync("showAlert", new { Name = "John", Age = 35 });

        //private async Task InsertParagraph() =>
        //    await _jsModule.InvokeVoidAsync("insertParagraph");
    }
}
