using AutoMapper;
using Stocks.Application.Dtos;
using Stocks.Application.Entities;
using Stocks.Application.Interfaces.IMapper;
using Stocks.Application.Mapper;
using Stocks.Application.Common;
using Stocks.Application.Extensions;

namespace Stocks.Presentation.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection RegisterMapperService(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Stock, StockDto>()
                    .ForMember(dest => dest.CarName, opt => opt.MapFrom(src => $"{src.Year} {src.Make} {src.Model} {src.FuelType}"))
                    .ForMember(dest => dest.FormattedPrice, opt => opt.MapFrom(src => PriceFormatter.FormatPrice(src.Price)))
                    .ForMember(dest => dest.IsValueForMoney, opt => opt.MapFrom(src => src.Kms < 10000 && src.Price < 200000));
                cfg.CreateMap<FiltersDto, Filter>()
                    .ForMember(dest => dest.MinBudget, opt => opt.MapFrom<MinBudgetResolver>())
                    .ForMember(dest => dest.MaxBudget, opt => opt.MapFrom<MaxBudgetResolver>())
                    .ForMember(dest => dest.FuelTypes, opt => opt.MapFrom<FuelTypeResolver>());
                cfg.CreateMap<Filter, List<IndividualFilter>>()
                    .ConvertUsing<FilterToIndividualFiltersConverter>();
                cfg.CreateMap<StockCreateDto, Stock>()
                    .ForMember(dest => dest.FuelType, opt => opt.MapFrom(src => EnumExtensions.FromString(src.FuelType)));
                cfg.CreateMap<StockUpdateDto, Stock>();
            }).CreateMapper());

            // Wrapper for IMapper
            services.AddSingleton<IBaseMapper<Stock, StockDto>, BaseMapper<Stock, StockDto>>();
            services.AddSingleton<IBaseMapper<StockCreateDto, Stock>, BaseMapper<StockCreateDto, Stock>>();
            services.AddSingleton<IBaseMapper<StockUpdateDto, Stock>, BaseMapper<StockUpdateDto, Stock>>();
            services.AddSingleton<IBaseMapper<FiltersDto, Filter>, BaseMapper<FiltersDto, Filter>>();
            services.AddSingleton<IBaseMapper<Filter, List<IndividualFilter>>, BaseMapper<Filter, List<IndividualFilter>>>();

            return services;
        }
    }

    public class FilterToIndividualFiltersConverter : ITypeConverter<Filter, List<IndividualFilter>>
    {
        public List<IndividualFilter> Convert(Filter source, List<IndividualFilter> destination, ResolutionContext context)
        {
            var filters = new List<IndividualFilter>();

            // Map MinBudget
            if (source.MinBudget.HasValue)
            {
                filters.Add(new IndividualFilter
                {
                    PropertyName = "Price",
                    Comparison = Comparison.GreaterThanOrEqual,
                    Value = source.MinBudget.Value
                });
            }

            // Map MaxBudget
            if (source.MaxBudget.HasValue)
            {
                filters.Add(new IndividualFilter
                {
                    PropertyName = "Price",
                    Comparison = Comparison.LessThanOrEqual,
                    Value = source.MaxBudget.Value
                });
            }

            // Map FuelTypes
            if (source.FuelTypes?.Count > 0)
            {
                filters.Add(new IndividualFilter
                {
                    PropertyName = "FuelType",
                    Comparison = Comparison.In,
                    Value = source.FuelTypes
                });
            }

            return filters;
        }
    }
}