using Common.Models;
using Data.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDataFetcher _dataFetcher;
        private readonly string _baseUrl;
        private readonly ILogger<CompanyRepository> _logger;

        public CompanyRepository(IDataFetcher dataFetcher, ILogger<CompanyRepository> logger)
        {
            _logger = logger;
            _dataFetcher = dataFetcher;
            _baseUrl = Environment.GetEnvironmentVariable("CompanyBaseUrl");
            if (string.IsNullOrEmpty(_baseUrl))
            {
                _logger.LogError("Empty CompanyBaseUrl please check configuration");
                throw new ArgumentNullException("Company endponit url is not set");
            }
        }

        /// <summary>
        /// Method returns all avialable company
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            var response = await _dataFetcher.FetchDataAsync(_baseUrl);
            return JsonSerializer.Deserialize<List<Company>>(response);
        }
    }
}
