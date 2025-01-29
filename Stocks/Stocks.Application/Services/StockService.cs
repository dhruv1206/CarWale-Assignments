using Stocks.Application.Dtos;
using Stocks.Application.Entities;
using Stocks.Application.Common;

using Stocks.Application.Interfaces.IRepositories;
using Stocks.Application.Interfaces.IServices;
using Stocks.Application.Interfaces.IMapper;

namespace Stocks.Application.Services
{
    public class StockService : BaseService<Stock, StockDto, FiltersDto, Filter>, IStockService
    {
        private readonly IBaseMapper<Stock, StockDto> _stockToStockDtoMapper;
        private readonly IBaseMapper<StockCreateDto, Stock> _stockCreateDtoToStockMapper;
        private readonly IBaseMapper<StockUpdateDto, Stock> _stockUpdateDtoToStockMapper;
        private readonly IBaseMapper<Filter, List<IndividualFilter>> _filterToIndividualFilterMapper;
        private readonly IBaseMapper<FiltersDto, Filter> _filterDtoToFilterMapper;

        private readonly IStockRepository _stockRepository;

        public StockService(
            IBaseMapper<Stock, StockDto> stockToStockDtoMapper,
            IBaseMapper<StockCreateDto, Stock> stockCreateDtoToStockMapper,
            IBaseMapper<StockUpdateDto, Stock> stockUpdateDtoToStockMapper,
            IBaseMapper<FiltersDto, Filter> filterDtoToFilterMapper,
            IBaseMapper<Filter, List<IndividualFilter>> filterToIndividualFilterMapper,
            IStockRepository stockRepository)
            : base(stockToStockDtoMapper, filterDtoToFilterMapper, filterToIndividualFilterMapper, stockRepository)
        {
            _stockToStockDtoMapper = stockToStockDtoMapper;
            _stockCreateDtoToStockMapper = stockCreateDtoToStockMapper;
            _stockUpdateDtoToStockMapper = stockUpdateDtoToStockMapper;
            _stockRepository = stockRepository;
            _filterDtoToFilterMapper = filterDtoToFilterMapper;
            _filterToIndividualFilterMapper = filterToIndividualFilterMapper;
        }

        public async Task<StockDto> Create(StockCreateDto stockCreateDto)
        {
            var stock = _stockCreateDtoToStockMapper.MapModel(stockCreateDto);
            var createdStock = await _stockRepository.Create(stock);
            return _stockToStockDtoMapper.MapModel(createdStock);
        }
        public async Task<StockDto> Update(int id, StockUpdateDto stockUpdateDto)
        {
            Console.WriteLine("Updating stock with id: " + id);
            var existingStock = await _stockRepository.GetById(id);
            var stock = existingStock.CopyWith(stockUpdateDto);
            Console.WriteLine("Stock updated: " + stock);
            var updatedStock = await _stockRepository.Update(stock);
            return _stockToStockDtoMapper.MapModel(updatedStock);
        }
    }

}