using Common.Models;
using Data.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ProcessCompanyOrders.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataFetcher _dataFetcher;
        private readonly string _baseUrl;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IDataFetcher dataFetcher, ILogger<ProductRepository> logger)
        {
            _logger = logger;
            _dataFetcher = dataFetcher;
            _baseUrl = Environment.GetEnvironmentVariable("ProductBaseUrl");
            if (string.IsNullOrEmpty(_baseUrl))
            {
                _logger.LogError("Empty Productbaseurl please check configuration");
                throw new ArgumentNullException("Product endponit url is not set");
            }
        }

        /// <summary>
        /// Method return all avialable products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var response = await _dataFetcher.FetchDataAsync(_baseUrl);
            return JsonSerializer.Deserialize<List<Product>>(response);
        }
    }
}
