using System.ComponentModel.DataAnnotations;
using Stocks.Application.Entities;
using Stocks.Application.Validations;

namespace Stocks.Application.Dtos
{
    public class StockCreateDto
    {
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Kms { get; set; }
        [Required]
        [FuelTypeValidationAttribute]
        public string FuelType { get; set; }
        public override string ToString()
        {
            return $"Make: {Make}, Model: {Model}, Year: {Year}, Price: {Price}, Kms: {Kms}, FuelType: {FuelType}";
        }
    }
}
