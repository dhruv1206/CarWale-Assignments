using System.Threading.Tasks;
using Stocks.Application.Entities;

namespace Stocks.Application.Interfaces.IRepositories
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        Task<Stock> Create(Stock stock);
        Task<Stock> Update(Stock stock);
    }
}