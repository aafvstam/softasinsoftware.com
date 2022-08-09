namespace softasinsoftware.Shared.Models
{
    public class UploadResult
    {
        public string? FileNameOriginal { get; set; }
        public string? FileNameStored { get; set; }
        public bool Uploaded { get; set; }
        public int ErrorCode { get; set; }
    }
}