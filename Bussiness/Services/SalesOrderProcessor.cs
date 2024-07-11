using Data.Interfaces;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Xml;
namespace Bussiness
{
    public class SalesOrderProcessor : IProcessData
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUploader _uploader;
        private readonly ILogger<SalesOrderProcessor> _logger;

        public SalesOrderProcessor(ISalesOrderRepository salesOrderRepository, IProductRepository productRepository,
            ICompanyRepository companyRepository, IUploader uploader, ILogger<SalesOrderProcessor> logger)
        {
            _salesOrderRepository = salesOrderRepository;
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _uploader = uploader;
            _logger = logger;
        }

        /// <summary>
        /// Method gets sales order details from company id and merges product based on product id from sales order
        /// Method then uploads the json file to Blob Storage
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ProcessDataAsync()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();
            var products = await _productRepository.GetAllProductsAsync();
            
            if (companies == null)
            {
                _logger.LogError("No companies found, process is aborted");
                return false;
            }

            if (products == null)
            {
                _logger.LogError("No products found, process is aborted");
                return false;
            }

            foreach (var company in companies)
            {
                var salesOrders = await _salesOrderRepository.GetSalesOrdersAsync(company.CompanyId);
                foreach (var order in salesOrders)
                {
                    order.ProductDetails = products.FirstOrDefault(p => p.ItemId == order.ItemId);
                }
                var companyData = new
                {
                    Company = company,
                    SalesOrders = salesOrders
                };
                var jsonData = JsonSerializer.Serialize(companyData, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                if (!await _uploader.UploadDataAsync($"{company.Name}.json", jsonData))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
