using Bussiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;


namespace FunctionAppOrders.Functions
{
    public class ProcessOrder
    {
        private readonly ILogger<ProcessOrder> _logger;
        private readonly IProcessData _dataProcessor;

        public ProcessOrder(ILogger<ProcessOrder> logger, IProcessData dataProcessor)
        {
            _logger = logger;
            _dataProcessor = dataProcessor;
        }

        /// <summary>
        /// Function process order request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>

        [Function("ProcessOrderFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Started processing request to fetch and process order data");
            try
            {
                var isSuccess = await _dataProcessor.ProcessDataAsync();
                if (isSuccess) {
                    return new OkObjectResult("Orders are processed successfully, please have a look at Blob Storage for new order json files");
                }
                return new BadRequestObjectResult("Order processing was not successfully, please check log for more details");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred: {ex.Message}", ex.Message);
                return new BadRequestObjectResult($"Error in processing request - {ex.Message}");
            }
        }
    }
}
