using Stocks.Application.Interfaces.IServices;
using Stocks.Application.Interfaces.IRepositories;
using Stocks.Application.Services;
using Stocks.Data.Repositories;


namespace Stocks.Presentation.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            // SERVICES
            services.AddScoped<IStockService, StockService>();
            // REPOS
            services.AddScoped<IStockRepository, StockRepository>();
            return services;
        }
    }
}