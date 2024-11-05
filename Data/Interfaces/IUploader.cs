namespace Data.Interfaces
{
    /// <summary>
    /// Test
    /// </summary>
    public interface IUploader
    {
        Task<bool> UploadDataAsync(string filename, string data);
    }
}
