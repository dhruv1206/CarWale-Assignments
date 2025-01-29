using Microsoft.VisualBasic.CompilerServices;
using Moq;
using Stocks.Application.Entities;
using Stocks.Application.Dtos;
using Stocks.Application.Interfaces.IMapper;
using Stocks.Application.Common;
using Stocks.Application.Interfaces.IRepositories;
using Stocks.Application.Services;



namespace Stocks.UnitTests.Application
{
    public class PublicApplicationTests
    {
        private readonly Mock<IBaseMapper<Stock, StockDto>> _stockToStockDtoMapper = new();
        private readonly Mock<IBaseMapper<StockCreateDto, Stock>> _stockCreateDtoToStockMapper = new();
        private readonly Mock<IBaseMapper<StockUpdateDto, Stock>> _stockUpdateDtoToStockMapper = new();
        private readonly Mock<IBaseMapper<FiltersDto, Filter>> _filtersDtoToFiltersMapper = new();
        private readonly Mock<IBaseMapper<Filter, List<IndividualFilter>>> _filtersToIndividualFiltersMapper = new();

        private readonly Mock<IStockRepository> _stockRepository = new();


        [Fact]
        public async Task CreateStockAsync_ValidProduct_ReturnsCreatedStockDto()
        {
            // Arrange
            var _stockService = new StockService(
                _stockToStockDtoMapper.Object,
                _stockCreateDtoToStockMapper.Object,
                _stockUpdateDtoToStockMapper.Object,
                _filtersDtoToFiltersMapper.Object,
                _filtersToIndividualFiltersMapper.Object,
                _stockRepository.Object
            );
            var _stockCreateDto = new StockCreateDto
            {
                Make = "Toyota",
                Model = "Corolla",
                Year = "2021",
                Price = 10000,
                Kms = 1000,
                FuelType = "CNG"
            };
            var _stockDto = new StockDto
            {
                Id = 1,
                FormattedPrice = "Rs. 10 Thousand",
                CarName = "2021 Toyota Corolla CNG",
                IsValueForMoney = false
            };
            var _createdStock = new Stock
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corolla",
                Year = "2021",
                Price = 10000,
                Kms = 1000,
                FuelType = FuelType.CNG
            };
            _stockCreateDtoToStockMapper.Setup(x => x.MapModel(_stockCreateDto)).Returns(_createdStock);
            _stockRepository.Setup(x => x.Create(_createdStock)).ReturnsAsync(_createdStock);
            _stockToStockDtoMapper.Setup(x => x.MapModel(_createdStock)).Returns(_stockDto);

            // Act
            var result = await _stockService.Create(_stockCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_stockDto, result);
        }
        [Fact]
        public async Task UpdateStockAsync_ValidProduct_ReturnsUpdatedStockDto()
        {
            // Arrange
            var _stockService = new StockService(
                _stockToStockDtoMapper.Object,
                _stockCreateDtoToStockMapper.Object,
                _stockUpdateDtoToStockMapper.Object,
                _filtersDtoToFiltersMapper.Object,
                _filtersToIndividualFiltersMapper.Object,
                _stockRepository.Object
            );
            var _stockUpdateDto = new StockUpdateDto
            {
                Make = "Toyota",
                Model = "Corolla",
                Year = "2021",
                Price = 10000,
                Kms = 1000,
                FuelType = "CNG"
            };
            var _stockDto = new StockDto
            {
                Id = 1,
                FormattedPrice = "Rs. 10 Thousand",
                CarName = "2021 Toyota Corolla CNG",
                IsValueForMoney = false
            };
            var _updatedStock = new Stock
            {
                Id = 1,
                Make = "Toyota",
                Model = "Corolla",
                Year = "2021",
                Price = 10000,
                Kms = 1000,
                FuelType = FuelType.CNG
            };
            _stockUpdateDtoToStockMapper.Setup(x => x.MapModel(_stockUpdateDto)).Returns(_updatedStock);
            _stockRepository.Setup(x => x.Update(
                It.IsAny<Stock>()
            )).ReturnsAsync(_updatedStock);
            _stockRepository.Setup(x => x.GetById(
                It.IsAny<int>())).ReturnsAsync(_updatedStock);

            _stockToStockDtoMapper.Setup(x => x.MapModel(_updatedStock)).Returns(_stockDto);

            // Act
            var result = await _stockService.Update(1, _stockUpdateDto);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(_stockDto, result);
        }
    }
}