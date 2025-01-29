using System.ComponentModel.DataAnnotations;
using Stocks.Application.Validations;

namespace Stocks.Application.Dtos
{
    public class StockUpdateDto
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public int? Price { get; set; }
        public int? Kms { get; set; }
        [FuelTypeValidationAttribute]
        public string? FuelType { get; set; }
    }
}