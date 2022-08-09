using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using softasinsoftware.Shared.Models;

using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace softasinsoftware.Web.Pages.Gear
{
    partial class Edit
    {
        [Parameter]
        public int gearId { get; set; }
        protected string Title = "Add";

        public GearItem GearItem { get; set; } = new();

        private string? imagesource = string.Empty;

        public string? ImageBaseAddress { get; set; } = string.Empty;

        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        [Inject]
        public NavigationManager? NavigationManager { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            if (gearId != 0)
            {
                Title = "Edit";

                if (ClientFactory == null)
                {
                    return;
                }

                var client = ClientFactory.CreateClient("softasinsoftware.API");

                if (client == null)
                {
                    return;
                }

                if (client.BaseAddress != null)
                {
                    ImageBaseAddress = client.BaseAddress.ToString();
                }

                HttpResponseMessage response = await client.GetAsync("gear/" + Convert.ToInt32(gearId));

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();

                    if (responseStream != null)
                    {
                        var options = new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        GearItem? gearitem = await JsonSerializer.DeserializeAsync<GearItem>(responseStream, options);

                        if (gearitem != null)
                        {
                            GearItem = gearitem;
                            imagesource = ImageBaseAddress + $"imagedownload/" + GearItem.Image;
                        }
                    }
                }
            }
        }

        protected async Task SaveGearItem()
        {
            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            HttpResponseMessage response = new();

            if (gearId != 0)
            {
                response = await client.PutAsJsonAsync("gear/" + Convert.ToInt32(gearId), GearItem);
            }
            else
            {
                response = await client.PostAsJsonAsync("gear/", GearItem);
            }

            if (response.IsSuccessStatusCode)
            {
                this.NavigationManager.NavigateTo("/gear/gear");
            }

            Cancel();
        }

        public void Cancel()
        {
            this.NavigationManager.NavigateTo("/gear/gear");
        }

        private bool shouldRender;
        private bool isLoading;

        private int maxAllowedFiles = 5;
        private long maxFileSize = 1024 * 1024 * 15;

        private List<UploadFile> loadedFiles = new();
        private List<UploadResult> uploadResults = new();

        string baseaddress = string.Empty;


        //Date Added: 20220727
        //--------------------  
        //TODO: Change from Multiple Upload to Single Upload
        //TODO: On Edit show the full Image info panel
        //TODO: On a second upload the first upload should be removed from disk
        //TODO: Fix Stream Layout bit better ... suggested by @skod
        //TODO: Add link to Azure Shield Cert
        //TODO: Add shields 👆 a component?
        //TODO: Make the Upload a separate Blazor Component
        //TODO: When deleting the Gear Item also delete the 'attached' image 
        //TODO: Update the Offline message on Twitch (very old picture/logo there)
        //TODO: Remove Static Identity parts? https://github.com/dotnet/AspNetCore.Docs/pull/16560/commits/b6d3e7f90954fe7c89fd59e677181de71376ced7

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();

            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            if (client == null)
            {
                return;
            }

            if (client.BaseAddress != null)
            {
                baseaddress = client.BaseAddress.ToString();
            }

            shouldRender = false;

            var upload = false;

            MultipartFormDataContent multipartFormDataContent = new();
            using MultipartFormDataContent? formData = multipartFormDataContent;

            try
            {
                foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
                {
                    if (uploadResults.SingleOrDefault(f => f.FileNameOriginal == file.Name) is null)
                    {
                        try
                        {
                            var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));

                            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                            loadedFiles.Add(new() { Name = file.Name });

                            formData.Add(content: fileContent, name: "\"files\"", fileName: file.Name);

                            upload = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"'{file.Name}' not uploaded (Err: 6): {ex.Message}");

                            uploadResults.Add(
                                new()
                                {
                                    FileNameOriginal = file.Name,
                                    FileNameStored = string.Empty,
                                    ErrorCode = 6,
                                    Uploaded = false
                                });
                        }
                    }
                }

                if (upload)
                {
                    try
                    {
                        HttpResponseMessage? response = await client.PostAsync("filesave", multipartFormDataContent);
                        var newUploadResults = await response.Content.ReadFromJsonAsync<IList<UploadResult>>();

                        if (newUploadResults is not null)
                        {
                            uploadResults = uploadResults.Concat(newUploadResults).ToList();
                            if (uploadResults.Any())
                            {
                                imagesource = baseaddress + "imagedownload/" + uploadResults[0].FileNameStored;
                                GearItem.Image = uploadResults[0].FileNameStored;
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }

                isLoading = false;
                shouldRender = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Too many files (Err: 7): {ex.Message}");
            }
        }

        private static bool FileUpload(IList<UploadResult> uploadResults,
                                string? fileName,
                                out UploadResult result)
        {
            result = uploadResults.SingleOrDefault(f => f.FileNameOriginal == fileName) ?? new UploadResult();

            if (!result.Uploaded)
            {
                result.ErrorCode = 5;
                Console.WriteLine($"{fileName} not uploaded (Err: {result.ErrorCode})");
            }

            return result.Uploaded;
        }
    }
}
