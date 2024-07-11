namespace Data.Interfaces
{
    /// <summary>
    /// Interface DataFetcher
    /// </summary>
    public interface IDataFetcher
    {
        Task<string> FetchDataAsync(string url);
    }
}
