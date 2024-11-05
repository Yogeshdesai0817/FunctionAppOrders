namespace Data.Interfaces
{
    /// <summary>
    /// Test test 1 test 2
    /// </summary>
    public interface IUploader
    {
        Task<bool> UploadDataAsync(string filename, string data);
    }
}
