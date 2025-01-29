using System.ComponentModel.DataAnnotations;
using Stocks.Application.Dtos;
using Stocks.Application.Entities;
using Stocks.Application.Extensions;

namespace Stocks.Application.Entities
{
    public class Stock : Base<int>
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
        public FuelType FuelType { get; set; }
        public override string ToString()
        {
            return $"Make: {Make}, Model: {Model}, Year: {Year}, Price: {Price}, Kms: {Kms}, FuelType: {FuelType}";
        }

        public Stock CopyWith(Stock stock)
        {
            return new Stock
            {
                Id = stock.Id == null ? Id : stock.Id,
                Make = stock.Make == null ? Make : stock.Make,
                Model = stock.Model == null ? Model : stock.Model,
                Year = stock.Year == null ? Year : stock.Year,
                Price = stock.Price == null ? Price : stock.Price,
                Kms = stock.Kms == null ? Kms : stock.Kms,
                FuelType = stock.FuelType == null ? FuelType : stock.FuelType
            };
        }

        public Stock CopyWith(StockUpdateDto stock)
        {
            return new Stock
            {
                Id = this.Id,
                Make = stock.Make == null ? Make : stock.Make,
                Model = stock.Model == null ? Model : stock.Model,
                Year = stock.Year == null ? Year : stock.Year,
                Price = stock.Price == null ? Price : (int)stock.Price,
                Kms = stock.Kms == null ? Kms : (int)stock.Kms,
                FuelType = stock.FuelType == null ? FuelType : (FuelType)EnumExtensions.FromString(stock.FuelType)
            };
        }
    }
}

