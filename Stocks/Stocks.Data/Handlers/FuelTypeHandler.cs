using Dapper;
using System.Data;
using Stocks.Application.Entities;

namespace Stocks.Data.Handlers
{
    public class FuelTypeHandler : SqlMapper.TypeHandler<FuelType>
    {
        public override FuelType Parse(object value) =>
                    Enum.Parse<FuelType>(value.ToString()!, ignoreCase: true);
        public override void SetValue(IDbDataParameter parameter, FuelType value) =>
            parameter.Value = value.ToString();
    }
}