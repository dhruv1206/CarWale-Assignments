using System.Threading.Tasks;
using Stocks.Application.Dtos;
using Stocks.Application.Entities;


namespace Stocks.Application.Interfaces.IServices
{
    public interface IStockService : IBaseService<StockDto, FiltersDto>
    {
        Task<StockDto> Create(StockCreateDto stockCreateDto);
        Task<StockDto> Update(int id, StockUpdateDto stockUpdateDto);

    }
}