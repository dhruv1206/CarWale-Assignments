using Stocks.Application.Dtos;
using Stocks.Application.Entities;

using AutoMapper;

namespace Stocks.Application.Mapper
{
    public class MinBudgetResolver : IValueResolver<FiltersDto, Filter, int?>
    {
        public int? Resolve(FiltersDto source, Filter destination, int? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Budget) && source.Budget.Contains('-') &&
                int.TryParse(source.Budget.Split('-')[0], out var MinBudget))
            {
                return MinBudget;
            }
            return null;
        }
    }

    public class MaxBudgetResolver : IValueResolver<FiltersDto, Filter, int?>
    {
        public int? Resolve(FiltersDto source, Filter destination, int? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Budget) && source.Budget.Contains('-') &&
                int.TryParse(source.Budget.Split('-')[1], out var MaxBudget))
            {
                return MaxBudget;
            }
            return null;
        }
    }

    public class FuelTypeResolver : IValueResolver<FiltersDto, Filter, List<FuelType>>
    {
        public List<FuelType> Resolve(FiltersDto source, Filter destination, List<FuelType> destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.FuelType))
            {
                return new List<FuelType>();
            }

            return source.FuelType.Split('+')
                .Select(f => Enum.TryParse<FuelType>(f.Trim(), true, out var result) ? result : (FuelType?)null)
                .Where(f => f != null)
                .Cast<FuelType>()
                .ToList();
        }
    }

}