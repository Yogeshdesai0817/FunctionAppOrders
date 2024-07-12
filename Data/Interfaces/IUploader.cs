namespace Data.Interfaces
{
    public interface IUploader
    {
        Task<bool> UploadDataAsync(string filename, string data);
    }
}
