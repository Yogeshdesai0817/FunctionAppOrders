using Bussiness.Interfaces;
using Bussiness.Services;
using Bussiness;
using Data.Interfaces;
using Data.Repositories;
using Data.Services;
using Microsoft.Extensions.DependencyInjection;
using ProcessCompanyOrders.Repositories;
using ProcessCompanyOrders.Services;

namespace FunctionAppOrders.Utils
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataFetcher, HttpClientFetcher>();
            services.AddSingleton<IUploader, BlobStorageDataUploader>();
            services.AddSingleton<IMessageSender, MessageBusHandler>();
            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ISalesOrderRepository, SalesOrderRepository>();
            services.AddSingleton<IProcessData, SalesOrderProcessor>();
            services.AddSingleton<IMessageSenderService, MessageSenderService>();
            services.AddSingleton<IMessageUploaderService, MessageUploaderService>();
            return services;
        }
    }
}
