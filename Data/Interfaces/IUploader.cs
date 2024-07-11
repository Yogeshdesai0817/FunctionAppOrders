namespace Data.Interfaces
{
    /// <summary>
    /// Interface uploader
    /// </summary>
    public interface IUploader
    {
        Task<bool> UploadDataAsync(string filename, string data);
    }
}
