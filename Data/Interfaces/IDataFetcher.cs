namespace Data.Interfaces
{
    public interface IDataFetcher
    {
        Task<string> FetchDataAsync(string url);
    }
}
