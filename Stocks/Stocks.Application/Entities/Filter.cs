namespace Stocks.Application.Entities
{
    public class Filter
    {
        public int? MinBudget { get; set; }
        public int? MaxBudget { get; set; }
        public List<FuelType> FuelTypes { get; set; }
    }

    public enum FuelType
    {
        Petrol,
        Diesel,
        CNG,
        LPG,
        Electric,
        Hybrid
    }
}