using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using softasinsoftware.Shared.Models;

using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace softasinsoftware.Web.Pages
{
    public partial class FileUpload2Page
    {
        private List<UploadFile> loadedFiles = new();
        private List<UploadResult> uploadResults = new();

        private int maxAllowedFiles = 5;
        private long maxFileSize = 1024 * 1024 * 15;
        private bool shouldRender;
        private bool isLoading;

        string baseaddress = string.Empty;
        string imagesource = string.Empty;

        protected override bool ShouldRender() => shouldRender;

        [Inject]
        public IHttpClientFactory? ClientFactory { get; private set; }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();

            if (ClientFactory == null)
            {
                return;
            }

            var client = ClientFactory.CreateClient("softasinsoftware.API");

            baseaddress = client.BaseAddress.ToString();

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
