namespace Stocks.Application.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }
        public string FormattedPrice { get; set; }
        public string CarName { get; set; }
        public bool IsValueForMoney { get; set; }
    }
}
