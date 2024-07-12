using Common.Models;
using Data.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ProcessCompanyOrders.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly IDataFetcher _dataFetcher;
        private readonly string _baseUrl;
        private readonly ILogger<SalesOrderRepository> _logger;


        public SalesOrderRepository(IDataFetcher dataFetcher, ILogger<SalesOrderRepository> logger)
        {
            _logger = logger;
            _dataFetcher = dataFetcher;
            _baseUrl = Environment.GetEnvironmentVariable("SalesOrderBaseUrl");
            if (string.IsNullOrEmpty(_baseUrl))
            {
                _logger.LogError("Empty SalesOrderBaseUrl please check configuration");
                throw new ArgumentNullException("Salesorder endponit url is not set");
            }
        }

        /// <summary>
        /// Method returns all sales order based on company id.
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync(int companyId)
        {
            var response = await _dataFetcher.FetchDataAsync($"{_baseUrl}{companyId}");
            return JsonSerializer.Deserialize<List<SalesOrder>>(response);
        }
    }
}
